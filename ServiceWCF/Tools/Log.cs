using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceWCF.Tools
{
    class Log
    {
        public class Const
        {
            public const string DATE_FORMAT = "yyyyMMdd";
            public const string TIME_FORMAT = "HHmmss";
            public const string DEFAULT_APPLICATION_NAME = "ServiceWCF";
            public const string WINDOWS_LOG_NAME = "ServiceWCF";
            public const string DEFAULT_LOG_FILENAME = "Log_ServiceWCF.log";
        }

        private bool _windowsLogInitialized;
        private string _logFilePath;

        public Log()
        {
            this._windowsLogInitialized = false;
            this._logFilePath = System.IO.Path.Combine(System.Environment.CurrentDirectory, Const.DEFAULT_LOG_FILENAME);
        }

        public void AddEventLog(string message, 
                                EventLogEntryType logType = EventLogEntryType.Information,
                                string moduleName = "", 
                                int codeErreur = 0, 
                                bool addFileEventLog = true, 
                                bool addWindowsEventLog = false)
        {
            if (addFileEventLog) AddFileEventLog(message, logType, moduleName, codeErreur);
            if (addWindowsEventLog) AddWindowsEventLog(message, logType, moduleName, codeErreur);
        }

        private void AddFileEventLog(string message, EventLogEntryType logType = EventLogEntryType.Information, string moduleName = "", int codeErreur = 0, bool fromWindowsLogEvent = false)
	    {
		    System.IO.StreamWriter w = null;
		    System.Text.StringBuilder str = new System.Text.StringBuilder();

		    try {
			    string strPath = System.IO.Path.GetDirectoryName(_logFilePath);
			    if (!System.IO.Directory.Exists(strPath))
				    System.IO.Directory.CreateDirectory(strPath);

			    w = new System.IO.StreamWriter(_logFilePath, true, System.Text.Encoding.Default);

			    str.AppendFormat("[{0,-10:G} - {1,-8:G}]", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
			    str.AppendFormat(" {0}", Const.DEFAULT_APPLICATION_NAME);
			    if (!string.IsNullOrEmpty(moduleName))
				    str.AppendFormat(" ({0})", moduleName);
			    str.AppendFormat(" - {0}", logType.ToString());
			    if (codeErreur != 0)
				    str.AppendFormat(" - Code : {0}", codeErreur);

			    str.Append("\r\n");
			    str.Append(message);
			    str.Append("\r\n");

			    w.WriteLine(str.ToString());
		    } catch (Exception ex) {
			    if (!fromWindowsLogEvent) {
                    AddWindowsEventLog("Impossible d'écrire dans le fichier de log : " + _logFilePath + "\r\n" + message + "\r\nException : " + ex.Message, EventLogEntryType.Error, moduleName, 0, true);
			    }
		    } finally {
			    if ((w != null))
				    w.Close();
		    }
	    }

        private void AddWindowsEventLog(string message, EventLogEntryType logType = EventLogEntryType.Information, string moduleName = "", int codeErreur = 0, bool fromFileLogEvent = false)
        {
            EventLog evLog = new EventLog(Const.WINDOWS_LOG_NAME);

            message = string.Concat(message, "\r\n", "Module : ", moduleName);
            evLog.Source = Const.DEFAULT_APPLICATION_NAME;

            InitWindowsEventLog();

            try
            {
                evLog.WriteEntry(message, logType, codeErreur);
            }
            catch (Exception ex)
            {
                if (!fromFileLogEvent)
                {
                    AddFileEventLog("Impossible d'écrire dans le journal de log " + Log.Const.WINDOWS_LOG_NAME + ".\r\nLe journal doit être créé préalablement avec un compte Administrateur.\r\nMessage :\r\n" + message + "\r\nException : " + ex.Message, logType, moduleName, codeErreur, true);
                }
            }
            finally
            {
                evLog.Close();
            }
        }

        private void InitWindowsEventLog()
        {
            if (this._windowsLogInitialized)
                return;

            if (!InitWindowsEventSource(Const.DEFAULT_APPLICATION_NAME, Const.WINDOWS_LOG_NAME))
                return;

            // Modification des paramètres du journal d'évènement
            if (EventLog.Exists(Const.WINDOWS_LOG_NAME))
            {
                EventLog a = new EventLog(Const.WINDOWS_LOG_NAME);
                if (a.OverflowAction != OverflowAction.OverwriteAsNeeded)
                {
                    a.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);
                }

                if (a.MaximumKilobytes < 2048)
                {
                    a.MaximumKilobytes = 2048;
                }
            }

            this._windowsLogInitialized = true;
        }

        private bool InitWindowsEventSource(string source, string logName)
        {
            string l = "";

            try
            {
                // Cette fonction renvoie une exception de sécurité disant qu'on a pas les droits 
                // d'accéder au journal "Sécurité" de Windows quand on est pas admin du poste

                l = EventLog.LogNameFromSourceName(source, ".");
            }
            catch (System.Security.SecurityException)
            {
                return false;
            }

            if (l != logName)
            {
                try
                {
                    if (!string.IsNullOrEmpty(l))
                        EventLog.DeleteEventSource(source);
                    EventLog.CreateEventSource(source, logName);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
