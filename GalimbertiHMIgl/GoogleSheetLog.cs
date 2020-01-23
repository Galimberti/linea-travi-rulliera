using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Configuration;

namespace GalimbertiHMIgl
{

    
    public class GoogleSheetLog
    {
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        string sheet = "Sheet1";
        string SpreadsheetId = "1pm-xaJCfzx-MUjoitWsqGxsm_2CZ5OgulxENmhBJnyY";
        public SheetsService service;


        public void init()
        {

            if (this.service != null)
                return;

            SpreadsheetId = ConfigurationSettings.AppSettings.Get("SpreadsheetId");
            sheet = ConfigurationSettings.AppSettings.Get("Sheet");

            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GalimbertiHundegger",
            });
        }


        public void addLog(List<object> values)
        {
            char end = 'A';
            end += (char)(values.Count - 1);

            var range = $"{sheet}!A:"+end;
            var valueRange = new ValueRange();
            // Data for another Student...
            valueRange.Values = new List<IList<object>> { values };
            // Append the above record...
            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
        }

    }
}
