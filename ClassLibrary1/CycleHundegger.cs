using DatabaseInterface;
using PLCDrivers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using static DatabaseInterface.RullieraDB;

namespace GalimbertiHMIgl
{
    public class CycleHundegger
    {

        private PLC plcRulliera;
        PostgresLog log = new PostgresLog();
        FileLog filelog = new FileLog();

        DateTime lastSendPiece = new DateTime(); 

        public void init(PLC plc)
        {

            this.plcRulliera = plc;

            this.plcRulliera.pollActions.Add(
              () => {
                  this.plcRulliera.doWithPLC(c =>
                  {
                      bool presenza = c.readBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger");
                      bool presenzaAck = c.readBool("RULLI_CENTRO_TAGLI.WR_En_Anticipo_Pz_Hundegger");

                      if (presenza && presenzaAck)
                      {
                          c.writeBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", false);
                      }
                  });
              }
          );


            this.plcRulliera.pollActions.Add(
                () => {
                    this.plcRulliera.doWithPLC(c =>
                    {
                        bool presenza = c.readBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger");
                        bool presenzaAck = c.readBool("RULLI_CENTRO_TAGLI.WR_En_Anticipo_Pz_Hundegger");

                        if (presenza && presenzaAck)
                        {
                            c.writeBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", false);
                        }
                    });
                }
            );

            this.plcRulliera.pollActions.Add(
                () => {
                    this.plcRulliera.doWithPLC(c =>
                    {

                        bool presenza = c.readBool("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger");
                        bool presenzaAck = c.readBool("RULLI_CENTRO_TAGLI.WR_En_Scarico_Hundegger");
                        if (presenza && presenzaAck)
                        {
                            filelog.log("RISPOSTA DATI PLC SCARICO ----------------> OK");

                            c.writeBool("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger", false);
                        }
                    });
                }
            );
        }

      

        bool prev_WR_En_Anticipo_Hundegger = false;


        DateTime lastSent = DateTime.Now;

        private void _watcher_Created(Event ev)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ev.xml);

            XmlNode node = doc.DocumentElement.SelectSingleNode("/RectangularPart");
            Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".", "."));
            Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", "."));
            Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", "."));

            String ordiginKey = node.Attributes["Key"]?.InnerText;
            String name = node.Attributes["Name"]?.InnerText;
            String partId = node.Attributes["PartId"]?.InnerText;


            filelog.log("ELABORAZIONE SCARICO **************************************");
            filelog.log("Name :" + name);
            filelog.log("PartID :" + partId);
            filelog.log("Key :" + ordiginKey);

            TimeSpan lastElapsed = DateTime.Now.Subtract(lastSent);
            if (lastElapsed.TotalSeconds < 15)
            {
                filelog.log("ATTESA 10 SECONDI");
                Thread.Sleep(15*1000);
            }


            if (x < 600)
            {
                filelog.log("BUCA");
                return;
            }
               

            int rotation = 99;
            if (x > 1600)
            {
                 rotation = getRotation(doc, ordiginKey);
            }



            bool invertYZ = false;
            var nesting = this.rullieraDB.getNesting(ordiginKey);
            if (nesting != null)
            {
                var result = rotate(new Vector3(0, 0, 1), new Vector3(nesting.x, nesting.y, nesting.z));
                if ( Math.Abs(Vector3.Dot(result, new Vector3(0, 0, 1))) > 0.9)
                {
                    invertYZ = false;
                }else
                {
                    invertYZ = true;
                }
                this.filelog.log("INVERT YZ:" + invertYZ);
            }
            filelog.log("************************************************************");

            this.plcRulliera.doWithPLC(c =>
            {

                filelog.log("INVIO DATI PLC SCARICO *********************************************");
                filelog.log("Name :" + name);
                filelog.log("PartID :" + partId);
                filelog.log("Key :" + ordiginKey);

                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Rotazione_Pz_Da_Hundegger", rotation);

                if  (invertYZ)
                {
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Larghezza_Pz_Da_Hundegger", z);
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Altezza_Pz_Da_Hundegger", y);
                }
                else
                {
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Larghezza_Pz_Da_Hundegger", y);
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Altezza_Pz_Da_Hundegger", z);
                }

                c.writeBool("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger", true);

                filelog.log("***************************************************************");
                lastSent = DateTime.Now;
            });

         

        }

        RullieraDB rullieraDB = new RullieraDB();
        public void processDB()
        {
            try
            {

                foreach (Event ev in rullieraDB.getEventsToProcess())
                {
                    try
                    {

                        if (ev.event_type == "N")
                        {
                            filelog.log("Nesting");
                            this._watcher_Created_Nesting(ev);

                        }
                        else if (ev.event_type == "1")
                        {
                            filelog.log("Anticipo");
                            this._watcher_Created_Anticipo(ev);
                        }
                        else if (ev.event_type == "7")
                        {
                            filelog.log("Pezzo");
                            this._watcher_Created(ev);
                        }

                        this.rullieraDB.markAsProcessed(ev);
                    }
                    catch (Exception ex)
                    {
                        filelog.log(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                filelog.log(ex.Message);
            }
        }


        private void _watcher_Created_Nesting(Event ev)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ev.xml);

            var nodes = doc.DocumentElement.SelectNodes("//PartReference");

        
            foreach (XmlNode node in nodes)
            {

                var nodesRectPart = node.SelectNodes("//RectangularPart");

                foreach (XmlNode nodeRect in nodesRectPart)
                {
                    String referenceKey = nodeRect.Attributes["Key"]?.InnerText;
                    if (referenceKey != null)
                    {
                        var v = new Vector3();
                        v.X = (float)Double.Parse(node.Attributes["RotationX"]?.InnerText.Replace(".", "."));
                        v.Y = (float)Double.Parse(node.Attributes["RotationY"]?.InnerText.Replace(".", "."));
                        v.Z = (float)Double.Parse(node.Attributes["RotationZ"]?.InnerText.Replace(".", "."));
                        rullieraDB.logNesting(referenceKey, (int)v.X, (int)v.Y, (int)v.Z);
                    }
                } 
            }

        }

        private void _watcher_Created_Anticipo(Event ev)
        {
          
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ev.xml);

            XmlNode node = doc.DocumentElement.SelectSingleNode("/RectangularPart");

            Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".", "."));
            Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", "."));
            Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", "."));

            String ordiginKey = node.Attributes["Key"]?.InnerText;

            if (x < 600)
                return;

            int rotation = 99;
            if (x > 1600)
            {
                rotation = getRotation(doc, ordiginKey);
            }

            bool invertYZ = false;
            var nesting = this.rullieraDB.getNesting(ordiginKey);
            if (nesting != null)
            {
                var result = rotate(new Vector3(0, 0, 1), new Vector3(nesting.x, nesting.y, nesting.z));
                if (Math.Abs(Vector3.Dot(result, new Vector3(0, 0, 1))) > 0.9)
                {
                    invertYZ = false;
                }
                else
                {
                    invertYZ = true;
                }
            }



            this.plcRulliera.doWithPLC(c =>
            {

                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Rotazione_Anticipo_Pz_Da_Hundegger", rotation);

                if (invertYZ)
                {
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Larghezza_Anticipo_Pz_Da_Hundegger", z);
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Altezza_Anticipo_Pz_Da_Hundegger", y);
                }
                else
                {
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Larghezza_Anticipo_Pz_Da_Hundegger", y);
                    c.writeDouble("RULLI_CENTRO_TAGLI.RD_Altezza_Anticipo_Pz_Da_Hundegger", z);
                }

                c.writeBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", true);

            });

        }


        public int getRotation(XmlDocument doc, String key)
        {
            try
            {
                var nodes = doc.DocumentElement.SelectNodes("//StandardFrame[@Attributes='VisibleFace']");

                if (nodes.Count > 0)
                {

                    int frame = int.Parse(nodes[0].Attributes["FrameId"].InnerText);
                    Vector3 reference = new Vector3();

                    this.filelog.log("PZ:" + key);
                    this.filelog.log("FRAME:" + frame);

                    if (frame == 1)
                    {
                        reference = new Vector3(0, 0, 1);
                    }
                    if (frame == 2)
                    {
                        reference = new Vector3(0, 1, 0);
                    }
                    if (frame == 3)
                    {
                        reference = new Vector3(0, 0, -1);
                    }
                    if (frame == 4)
                    {
                        reference = new Vector3(0, -1, 0);
                    }

                    this.filelog.log("BASE VECTOR:" + reference.ToString());

                    int ret = 99;
                    var nesting = this.rullieraDB.getNesting(key);
                    if (nesting != null)
                    {
                        this.filelog.log("NESTING :" + new Vector3(nesting.x, nesting.y, nesting.z).ToString());
                        ret = calculateFinalRotation(reference, new Vector3(nesting.x,nesting.y,nesting.z));
                    }else
                    {
                        this.filelog.log("NESTING NOT FOUND !!!!:" + key);
                        ret = calculateFinalRotation(reference, new Vector3(0,0,0));
                    }
                    return ret;
                }
            }
            catch (Exception ex)
            {
                this.filelog.log(ex.Message);
            }
            return 99;
        }

        public float ConvertToRadians(float angle)
        {
            return (float) (Math.PI / 180) * angle;
        }

        public Vector3 rotate(Vector3 point, Vector3 rotation)
        {
            if (rotation != null)
            {
                point = Vector3.Transform(point, Matrix4x4.CreateRotationX(ConvertToRadians(rotation.X)));
                point = Vector3.Transform(point, Matrix4x4.CreateRotationY(ConvertToRadians(rotation.Y)));
                point = Vector3.Transform(point, Matrix4x4.CreateRotationZ(ConvertToRadians(rotation.Z)));
            }
            return point;
        }

        public int calculateFinalRotation(Vector3 point, Vector3 rotation)
        {
            try
            {

                point = rotate(point, rotation);
 
                this.filelog.log("REFERENCE ROTATION RESULT: " + point.ToString());

                int resultFrame = 0;

                if ( Vector3.Dot(point, new Vector3(0,0,1)) > 0.9)
                {
                    resultFrame = 1;
                }
                if (Vector3.Dot(point, new Vector3(0, 0, -1)) > 0.9)
                {
                    resultFrame = 3;
                }
                if (Vector3.Dot(point, new Vector3(0, 1, 0)) > 0.9)
                {
                    resultFrame = 2;
                }
                if (Vector3.Dot(point, new Vector3(0, -1, 0)) > 0.9)
                {
                    resultFrame = 4;
                }

                this.filelog.log("FRAME RESULT: " + resultFrame);

                String setting = ConfigurationSettings.AppSettings.Get("FrameId" + resultFrame);
               
                return int.Parse(setting);
               
            }
            catch (Exception ex)
            {
                this.filelog.log(ex.Message);
            }
            return 99;
        }

        public static bool IsFileReady(string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }



    }
}
