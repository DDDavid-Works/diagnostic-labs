using System;
using System.Collections.Generic;
using System.Configuration;

namespace DiagnosticLabsBLL.Services
{
    public class CommonFunctions
    {
        public CommonFunctions()
        {
        }

        public List<string> GendersComboBoxList()
        {
            return new List<string>() { "Male", "Female" };
        }

        public string GetFieldNameLabel(string textBoxName)
        {
            if (string.IsNullOrEmpty(textBoxName)) return null;

            switch (textBoxName)
            {
                case "CivilStatus":
                    return "Civil Status";
                case "ENT":
                    return "ENT (Ears, Nose, Throat)";
                case "INTEGUMENTARYSKIN":
                    return "Integumentary/Skin";
                case "OBGYNEUROLOGY":
                    return "OB-Gyne/Urology";
                case "INFECTIOUSCOMMUNICABLE":
                    return "Infectious/Communicable";
                case "OTHERS":
                    return "Others Past Medical History";
                case "MedicalSurgicalHistory":
                    return "Medical/Surgical History";
                default:
                    return char.ToUpper(textBoxName[0]) + textBoxName.Substring(1);
            }
        }

        public void LogMessage(string src, string msg)
        {
            string logFile = ConfigurationManager.AppSettings["LogFile"];
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(logFile, true))
            {
                file.WriteLine(src + " : " + msg);
            }
        }

        public void LogException(string src, Exception ex)
        {
            string logFile = $"{ConfigurationManager.AppSettings["LogFilePath"]}Log_{DateTime.Now.ToString("MMddyyyyy")}.txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(logFile, true))
            {
                file.WriteLine(src + " : " + ex.Message);

                if (ex.InnerException != null)
                    file.WriteLine(src + " : " + ex.InnerException.Message);
            }
        }

        public int ComputeAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }
    }
}
