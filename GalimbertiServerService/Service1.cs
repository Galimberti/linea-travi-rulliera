using GalimbertiHMIgl;
using PLCDrivers;
using PLCDrivers.Beckhoff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace GalimbertiServerService
{
    public partial class GalimbertiServer : ServiceBase
    {
        public GalimbertiServer()
        {
            InitializeComponent();
        }


        Runner runner = new Runner();
        protected override void OnStart(string[] args)
        {
            runner.init();
        }

        protected override void OnStop()
        {
        }
    }

    // Provide the ProjectInstaller class which allows 
    // the service to be installed by the Installutil.exe tool
    [RunInstaller(true)]
    public class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller _process;
        private ServiceInstaller _service;

        public ProjectInstaller()
        {
            _process = new ServiceProcessInstaller();
            _process.Account = ServiceAccount.NetworkService;
            _service = new ServiceInstaller();
            _service.ServiceName = "GalimbertiHundeggerCommService";
            Installers.Add(_process);
            Installers.Add(_service);
        }
    }
}
