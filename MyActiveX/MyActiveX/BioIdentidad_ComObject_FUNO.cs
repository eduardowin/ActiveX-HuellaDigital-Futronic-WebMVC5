using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace MyActiveX
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true), Guid("49DC43E2-3EDE-4B22-AB71-C281487C11DB")]
    public partial class BioIdentidad_ComObject_FUNO : IComOjbect_Firma, IObjectSafety
    {
        #region Constants
        // Constants for implementation of the IObjectSafety_Firma interface.
        private const int INTERFACESAFE_FOR_UNTRUSTED_CALLER = 0x00000001;
        private const int INTERFACESAFE_FOR_UNTRUSTED_DATA = 0x00000002;
        private const int S_OK = 0;
        #endregion

        [DllImport("user32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetForegroundWindow")]
        private static extern long GetForegroundWindow();
        [DllImport("FSBIO_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "capturar")]
        public static extern int capturar(out int calidad, int generarBmp, int generarWsq, int generarIso, int generarAnsi, int generarFtr, int lfdEnable, int lfdStrenght, int habilitarRepetir, int autoclose, long hParent);
        [DllImport("FSBIO_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "capturarEx")]
        public static extern int capturarEx(out int calidad, int generarBmp, int generarWsq, int generarIso, int generarAnsi, int generarFtr, int lfdEnable, int lfdStrenght, int habilitarRepetir, int autoclose, long hParent);
        [DllImport("FSBIO_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "verificar")]
        public static extern int verificar(out int calidad, int nivelSeguridad, int tipoTemplate, byte[] templateBase, int templateSize, out int verificacion, int generarBmp, int generarWsq, int generarIso, int generarAnsi, int generarFtr, int lfdEnable, int lfdStrenght, int habilitarRepetir, int autoclose, long hParent);
        [DllImport("FSBIO_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "mergeTemplates")]
        public static extern int mergeTemplates(byte[] template1, int template1Size, byte[] template2, int template2Size);
        [DllImport("FSBIO_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "verificarx2")]
        public static extern int verificarx2(out int calidad, int nivelSeguridad, int tipoTemplate, byte[] templateBase, int templateSize, byte[] templateBase2, int templateSize2, out int verificacion, int generarBmp, int generarWsq, int generarIso, int generarAnsi, int generarFtr, int lfdLevel, int lfdStrength, int habilitarRepetir, int autoclose, long hParent);

        #region IComEvents_Firma
        /// <summary>
        /// Raise the MyFirstEvent event. This method is not exposed through COM.
        /// </summary>
        /// <param name="args"></param>
        [ComVisible(false)]
        private void OnMyFirstEvent(string args)
        {
            if (MyFirstEvent != null)
            {
                MyFirstEvent(args);
            }
        }

        /// <summary>
        /// Delegate for the MyFirstEvent 
        /// </summary>
        /// <param name="args"></param>
        [ComVisible(false)]
        public delegate void MyFirstEventHandler(string args);

        /// <summary>
        /// Implements the event in IComEvents_Firma interface
        /// </summary>
        public event MyFirstEventHandler MyFirstEvent;
        #endregion

        #region IComObject operations
        /// <summary>
        /// Implements the MyFirstCommand method that's exposed in the IComObject interface.
        /// MyFirstEvent will be raised, with the given argument as a parameter.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public int MyFirstComCommand(string arg)
        {
            // Trigger the event MyFirstEvent
            OnMyFirstEvent(arg);

            return (int)DateTime.Now.Ticks;
        }
        public String CapturarBioIdentidad(string arg)
        {
            String vResultado = "";

            try
            {
                String vRutaArchivo = arg.Split('|')[0];
                Int32 vNum_Muestra = Convert.ToInt32(arg.Split('|')[1]);
                Int32 vopcionFormatoAnsi = Convert.ToInt32(arg.Split('|')[2]);
                Int32 vopcionFormatoWsq = Convert.ToInt32(arg.Split('|')[3]);

                byte[] template1;
                int template1Size;
                byte[] template2;
                int template2Size;
                int calidad;
                int resultado;
                int generarBmp = 0;
                int generarWsq = 0;
                int generarIso = 0;
                int generarAnsi = 0;
                int habilitarRepetir = 0, autoClose = 0;
                int lfdEnable = 0;
                int lfdStrenght = 0;
                long hWND;
                hWND = GetForegroundWindow();
                generarAnsi = 0;
                //    generarIso = 1;
                //    generarBmp = 1;
                //    generarWsq = 1;
                //    autoClose = 1;
                //    habilitarRepetir = 1;
                //    lfdEnable = 1;
                lfdStrenght = 1;
                //    resultado = capturarEx(out calidad, generarBmp, generarWsq, generarIso, generarAnsi, 0, lfdEnable,
                //      lfdStrenght, habilitarRepetir, autoClose, hWND);

                try
                {
                      generarAnsi = vopcionFormatoAnsi;
                      generarWsq = vopcionFormatoWsq;
                }
                catch (Exception ex)
                {
                    generarAnsi = 1;
                }

                autoClose = 1;
                var resultado1 = capturarEx(out calidad, generarBmp, generarWsq, generarIso, generarAnsi, 0, lfdEnable, lfdStrenght,
                    habilitarRepetir, autoClose, hWND);
                if (resultado1 == 0)
                {
                    //template1 = System.IO.File.ReadAllBytes("template.Ansi");
                    //template1Size = template1.Length;
                    String vResultadoBase64 = ConvertirBase64(generarBmp, generarWsq, generarIso, generarAnsi);
                    //var resultado2 = capturar(out calidad, generarBmp, generarWsq, generarIso, 1, 0, lfdEnable, lfdStrenght,
                    //    habilitarRepetir, autoClose, hWND);
                    //if (resultado2 == 0)
                    //{
                    //    template2 = File.ReadAllBytes("template.Ansi");
                    //    template2Size = template2.Length;
                    //    renombrarSalidas(2, generarBmp, generarWsq, generarIso, generarAnsi);
                    //    resultado = mergeTemplates(template1, template1Size, template2, template2Size);

                    //}
                    if (vResultadoBase64.Contains("OK|"))
                    {
                        vResultado = vResultadoBase64 + "|" + calidad;
                    }
                    else
                    {
                        vResultado = "Error|Problemas al Generar archivo de BioIdentidad:";
                    }

                }
                else
                {
                    vResultado = "Error|Problemas al Generar archivo de BioIdentidad:";
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\n");
                sb.AppendFormat("Exception Found:\n{0}Type: {1}", string.Empty, ex.GetType().FullName);
                sb.AppendFormat("\n{0}Message: {1}", string.Empty, ex.Message);
                sb.AppendFormat("\n{0}Source: {1}", string.Empty, ex.Source);
                sb.AppendFormat("\n{0}Stacktrace: {1}", string.Empty, ex.StackTrace);

                if (ex.InnerException != null)
                {
                    sb.AppendFormat("\n{0}InnerMessage: {1}", string.Empty, ex.InnerException.Message);
                    sb.Append("\n");
                }
                String vError = " Error : " + sb.ToString();

                Util_Log.EscribirLog(vError);
            }

            return vResultado;
        }
        /// <summary>
        /// Implements the Dispose method that's exposed in the IComObject interface.
        /// Since we're in the world of COM, we provide our own Dispose method, as 
        /// opposed to just implementing the IDisposable interface.
        /// </summary>
        /// 

        public String VerificarBioIdentidad(string arg)
        {
            String vResultadoValidacion = "";

            String vNombreArchivo = arg.Split('|')[0];
            String vArchivoStr64 =  arg.Split('|')[1];
            Int32 vopcionFormatoAnsi = Convert.ToInt32(arg.Split('|')[2]);
            Int32 vopcionFormatoWsq = Convert.ToInt32(arg.Split('|')[3]);
            
                int calidad;
                int resultado;
                int nivelSeguridad = 0;
                int generarBmp = 0;
                int generarWsq = 0;
                int generarIso = 0;
                int generarAnsi = 0;
                int tipoTemplate = 0; //0: Ansi, 1: Iso
                int templateSize;
                int lfdEnable = 0;
                int lfdStrenght = 0;
                long hWND;
                byte[] templateEntrada;
                int resultadoVerificacion;
                int habilitarRepetir = 0, autoClose = 0;
                generarAnsi = 0;
                autoClose = 1;
                lfdEnable = 0;

                try
                {
                    generarAnsi = vopcionFormatoAnsi;
                    generarWsq = vopcionFormatoWsq;
                }
                catch (Exception ex)
                {
                    generarAnsi = 1;
                }

                hWND = GetForegroundWindow();
                if (LeerArchivo(out templateEntrada, vArchivoStr64))
                {
                    templateSize = templateEntrada.Length;
                    tipoTemplate = 0;
                    resultado = verificar(out calidad, nivelSeguridad, tipoTemplate, templateEntrada, templateSize, out resultadoVerificacion, generarBmp, generarWsq, generarIso, generarAnsi, 0, lfdEnable, lfdStrenght, habilitarRepetir, autoClose, hWND);
                    if (resultado == 0)
                    {
                        
                        //Util_Log.EscribirLog("nivelSeguridad" + nivelSeguridad.ToString() + "tipoTemplate" + tipoTemplate.ToString() ) ;

                        String vResultadoBase64 = ConvertirBase64(generarBmp, generarWsq, generarIso, generarAnsi, 1);
                        //Byte[] bytes = File.ReadAllBytes("templa_v.ansi");
                        //String vBase64 = Convert.ToBase64String(bytes);

                        if (resultadoVerificacion == 1)

                            vResultadoValidacion = vResultadoBase64 + "|" + "Verificación exitosa\r\nCalidad :" + calidad + "|" + calidad + "|" + resultadoVerificacion;
                        else
                            vResultadoValidacion = vResultadoBase64 + "|" + "No coincide el template\r\nCalidad :" + calidad + "|" + calidad + "|" + resultadoVerificacion;
                    }
                    else
                    {
                        vResultadoValidacion = "Error|Error resultado:" + resultado.ToString();
                    }
                }
                else
                {
                    vResultadoValidacion = "Error|No ha cargado el template";
                }
            

            return vResultadoValidacion;
        }
        private bool LeerArchivo(out byte[] pTemplateEntrada,String pArchivoStr64)
        {
            string archivo;
            
            try
            {
                pTemplateEntrada = Convert.FromBase64String(pArchivoStr64);
                return true;
            }
            catch(Exception ex)
            {
                pTemplateEntrada = null;

                StringBuilder sb = new StringBuilder();
                sb.Append("\n");
                sb.AppendFormat("Exception Found:\n{0}Type: {1}", string.Empty, ex.GetType().FullName);
                sb.AppendFormat("\n{0}Message: {1}", string.Empty, ex.Message);
                sb.AppendFormat("\n{0}Source: {1}", string.Empty, ex.Source);
                sb.AppendFormat("\n{0}Stacktrace: {1}", string.Empty, ex.StackTrace);

                if (ex.InnerException != null)
                {
                    sb.AppendFormat("\n{0}InnerMessage: {1}", string.Empty, ex.InnerException.Message);
                    sb.Append("\n");
                }
                String vError = " Error : " + sb.ToString();

                Util_Log.EscribirLog(vError);
                return false;
            }
            
            return true;
        }
        public void Dispose()
        {
            //System.Windows.Forms.MessageBox.Show("MyComComponent is now disposed");
        }
        public String ConvertirBase64( Int32 generarBmp, Int32 generarWsq, Int32 generarIso, Int32 generarAnsi, Int32 verificado=0)
        {
            String vResultadoBase64 = "";
            String vResultadoBase64_Ansi = "";
            String vResultadoBase64_Wsq = "";
            try
            {
                if (generarBmp == 1)
                {
                    //Byte[] bytes = File.ReadAllBytes("huella.bmp");
                    //vResultadoBase64 = Convert.ToBase64String(bytes);
                }

                if (generarWsq == 1)
                {
                    if (verificado == 1)
                    {
                        Byte[] bytes = File.ReadAllBytes("huella_v.wsq");
                        vResultadoBase64_Wsq = Convert.ToBase64String(bytes);
                    }
                    else {
                        Byte[] bytes = File.ReadAllBytes("huella.wsq");
                        vResultadoBase64_Wsq = Convert.ToBase64String(bytes);
                    }
                }

                if (generarIso == 1)
                {
                    //Byte[] bytes = File.ReadAllBytes("template.iso");
                    //vResultadoBase64 = "OK|" + Convert.ToBase64String(bytes);
                }

                if (generarAnsi == 1)
                {
                    if (verificado == 1)
                    {
                        Byte[] bytes = File.ReadAllBytes("templa_v.ansi");
                        vResultadoBase64_Ansi =   Convert.ToBase64String(bytes);
                    }
                    else
                    {
                        Byte[] bytes = File.ReadAllBytes("template.ansi");
                        vResultadoBase64_Ansi =  Convert.ToBase64String(bytes);
                    }
                }

                vResultadoBase64 = "OK|" + vResultadoBase64_Wsq + "|" + vResultadoBase64_Ansi;
            }
            catch (Exception ex)
            {
                vResultadoBase64 = "";
                StringBuilder sb = new StringBuilder();
                sb.Append("\n");
                sb.AppendFormat("Exception Found:\n{0}Type: {1}", string.Empty, ex.GetType().FullName);
                sb.AppendFormat("\n{0}Message: {1}", string.Empty, ex.Message);
                sb.AppendFormat("\n{0}Source: {1}", string.Empty, ex.Source);
                sb.AppendFormat("\n{0}Stacktrace: {1}", string.Empty, ex.StackTrace);

                if (ex.InnerException != null)
                {
                    sb.AppendFormat("\n{0}InnerMessage: {1}", string.Empty, ex.InnerException.Message);
                    sb.Append("\n");
                }
                String vError = " Error : " + sb.ToString();
                vResultadoBase64 = "Error|" + sb.ToString();

                Util_Log.EscribirLog(vError);
            }

            return vResultadoBase64;
        }
        public String renombrarSalidas(Int32 muestra, Int32 generarBmp, Int32 generarWsq, Int32 generarIso, Int32 generarAnsi, String pNom_Archivo)
        {
            String vResultadoTransferencia = "";
            try
            {
                //String vrutaAlmacenada = "C:\\template_Edu_" + pNom_Archivo + "_" + muestra.ToString() + ".ansi";
                String vrutaAlmacenada =  pNom_Archivo;
                if (generarBmp == 1)
                {
                    File.Delete("huella" + muestra.ToString() + ".bmp");

                    File.Move("huella.bmp", "huella" + muestra.ToString() + ".bmp");
                }

                if (generarWsq == 1)
                {
                    File.Delete("huella" + muestra.ToString() + ".bmp");
                    File.Move("huella.wsq", "huella" + muestra.ToString() + ".wsq");
                }

                if (generarIso == 1)
                {
                    File.Delete("template" + muestra.ToString() + ".iso");
                    File.Move("template.iso", "template" + muestra.ToString() + ".iso");
                }

                if (generarAnsi == 1)
                {
                    File.Delete("template" + muestra.ToString() + ".ansi");
                    File.Move("template.ansi", vrutaAlmacenada);
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\n");
                sb.AppendFormat("Exception Found:\n{0}Type: {1}", string.Empty, ex.GetType().FullName);
                sb.AppendFormat("\n{0}Message: {1}", string.Empty, ex.Message);
                sb.AppendFormat("\n{0}Source: {1}", string.Empty, ex.Source);
                sb.AppendFormat("\n{0}Stacktrace: {1}", string.Empty, ex.StackTrace);

                if (ex.InnerException != null)
                {
                    sb.AppendFormat("\n{0}InnerMessage: {1}", string.Empty, ex.InnerException.Message);
                    sb.Append("\n");
                }
                String vError = " Error : " + sb.ToString();

                Util_Log.EscribirLog(vError);
            }

            return vResultadoTransferencia;

        }

        public void CerrarCapturador(){

            this.Dispose();
        }
        #endregion

        #region IObjectSafety_Firma Methods
        public int GetInterfaceSafetyOptions(ref Guid riid, out int pdwSupportedOptions, out int pdwEnabledOptions)
        {
            pdwSupportedOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER | INTERFACESAFE_FOR_UNTRUSTED_DATA;
            pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER | INTERFACESAFE_FOR_UNTRUSTED_DATA;
            return S_OK;   // return S_OK
        }

        public int SetInterfaceSafetyOptions(ref Guid riid, int dwOptionSetMask, int dwEnabledOptions)
        {
            return S_OK;   // return S_OK
        }
        #endregion
    }


    #region Interface definitions
    /// <summary>
    /// Defines the methods than can be called on an object deriving from this COM interface.
    /// Any class that wants to expose these methods should implement this interface.
    /// </summary>
    [Guid("1C2BDBBF-F139-42CD-9A25-3D53EC8F610C")]
    [ComVisible(true)]
    public interface IComOjbect_Firma
    {
        [DispId(0x10000001)]
        int MyFirstComCommand(string arg);
        String CapturarBioIdentidad(string arg);
        String VerificarBioIdentidad(string arg);

        [DispId(0x10000002)]
        void CerrarCapturador();
        
        void Dispose();
    }

    /// <summary>
    /// Defines events that will be raised from the associated COM object.
    /// Don't derive from this interface. Instead, ,ark any class that uses 
    /// this interface with the attribute [ComSourceInterfaces(typeof(IComEvents_Firma))].
    /// Any class that uses this interface should implement a public event called 
    /// MyFirstEvent using a delegate that returns void and accepts a single string 
    /// parameter called args. e.g. 
    ///     
    ///     public delegate void MyFirstEventHandler(string args);
    /// 
    /// </summary>
    [Guid("4FF35808-68B4-4A62-8BE8-5C2F3FF0B836")]
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IComEvents_Firma
    {
        [DispId(0x00000001)]
        void MyFirstEvent(string args);
    }

    /// <summary>
    /// Import the IObjectSaftety COM Interface. 
    /// See http://msdn.microsoft.com/en-us/library/aa768224(VS.85).aspx
    /// </summary>
    [ComImport]
    [Guid("C2A0DE4E-440F-4343-B298-699A006F4A1E")] // This is the only Guid that cannot be modifed in this file
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IObjectSafety
    {
        [PreserveSig]
        int GetInterfaceSafetyOptions(ref Guid riid, out int pdwSupportedOptions, out int pdwEnabledOptions);

        [PreserveSig]
        int SetInterfaceSafetyOptions(ref Guid riid, int dwOptionSetMask, int dwEnabledOptions);
    }
    #endregion
}
