using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace MyActiveX
{
    public class Util_Log
    {
        //public static string TransactionNumberSeq()
        //{
        //    string TextPath = ConfigurationSettings.AppSettings["LogPath"].ToString();
        //    string FileStr = "\\TransactionFile.txt";
        //    int intLine = 0;

        //    try
        //    {
        //        if (File.Exists(TextPath + FileStr))
        //        {
        //            StreamReader st = File.OpenText(TextPath + FileStr);
        //            string Line = st.ReadLine();

        //            if (Line.ToString() != "99999")
        //            {
        //                intLine = Int32.Parse(Line);
        //                intLine += 1;
        //            }
        //            else
        //            {
        //                intLine = 0;
        //            }

        //            st.Close();

        //            File.WriteAllText(TextPath + FileStr, intLine.ToString());
        //        }
        //        else
        //        {
        //            File.WriteAllText(TextPath + FileStr, intLine.ToString());
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        File.WriteAllText(TextPath + FileStr, intLine.ToString());
        //    }

        //    return intLine.ToString().PadLeft(6, '0');
        //}
        // FIN - NEW - 11122013...

        public static string IncidenciaNumberSeq()
        {
            string TextPath = "C:\\Log_BioIdentidad\\";
            string FileStr = "\\Incidencia.txt";
            int intLine = 0;

            if (File.Exists(TextPath + FileStr))
            {
                StreamReader st = File.OpenText(TextPath + FileStr);
                string Line = st.ReadLine();

                if (Line.ToString() != "99999")
                {
                    intLine = Int32.Parse(Line);
                    intLine += 1;
                }
                else
                {
                    intLine = 0;
                }

                st.Close();

                File.Delete(TextPath + FileStr);
                StreamWriter FileWriter = File.CreateText(TextPath + FileStr);
                FileWriter.WriteLine(intLine.ToString());
                FileWriter.Close();
            }
            else
            {
                if (!Directory.Exists(TextPath))
                {
                    Directory.CreateDirectory(TextPath);

                    DirectorySecurity security = Directory.GetAccessControl(TextPath);
                    SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                    security.AddAccessRule(new FileSystemAccessRule(
                        everyone,
                        FileSystemRights.Modify |
                        FileSystemRights.Synchronize,
                        InheritanceFlags.ContainerInherit |
                        InheritanceFlags.ObjectInherit,
                        PropagationFlags.None,
                        AccessControlType.Allow));

                    Directory.SetAccessControl(TextPath, security);
                }

                StreamWriter FileWriter = File.CreateText(TextPath + FileStr);
                FileWriter.WriteLine(intLine.ToString());
                FileWriter.Close();
            }
            return intLine.ToString().PadLeft(6, '0');
        }

        //public static string TraerDatoConfiguracion(string Key)
        //{
        //    return ConfigurationSettings.AppSettings[Key].ToString();
        //}

        public static string mostrarError(String p_Error)
        {
            String strError = p_Error.Replace("'", " ");
            strError = strError.Replace("\n", " ");
            strError = strError.Replace("\"", " ");
            strError = strError.Replace(",", " ");
            return strError;
        }

        public static String EscribirLog(String p_Error)
        {
            String TextPath = "C:\\Log_BioIdentidad\\";
            String FileStr = "\\Log-Incidencia-nPuc-BioIdentidad-" + DateTime.Today.ToShortDateString().Replace("/", "") + ".txt";
            String strHora = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + "==>";
            String strIncidencia = IncidenciaNumberSeq();

            if (System.IO.File.Exists(TextPath + FileStr))
            {
                StreamWriter FileWriter = File.AppendText(TextPath + FileStr);
                FileWriter.WriteLine("N° Incidencia: " + strIncidencia + " Hora: " + strHora + p_Error + "\r\n");
                FileWriter.Close();
            }
            else
            {
                StreamWriter FileWriter = File.CreateText(TextPath + FileStr);
                FileWriter.WriteLine("N° Incidencia: " + strIncidencia + " Hora: " + strHora + p_Error + "\r\n");
                FileWriter.Close();
            }

            return strIncidencia;
        }

        //public static string leerArchivo(String strFecha)
        //{
        //    String TextPath = TraerDatoConfiguracion("nLog_Incidencia");
        //    String FileStr = "\\Log-Incidencia-nPuc-" + strFecha.Replace("/", "") + ".txt";
        //    String strResultado = String.Empty;

        //    if (System.IO.File.Exists(TextPath + FileStr))
        //    {
        //        strResultado = File.ReadAllText(TextPath + FileStr);
        //    }

        //    return strResultado;
        //}
  

    }
}
