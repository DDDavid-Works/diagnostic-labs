using DiagnosticLabs.Constants;
using DiagnosticLabsBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DiagnosticLabs
{
    public class CommonFunctions
    {
        SingleLineEntriesBLL singleLineEntriesBLL = new SingleLineEntriesBLL();

        public string ConfirmDeleteQuestion(string entity)
        {
            return $"Are you sure you want to delete this {entity.ToLower()}?";
        }

        public string HashPassword(string password)
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);

                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }

        public List<string> GeneralSingleLineEntryList(string fieldName, bool addNewEntryOption = false)
        {
            List<string> values = singleLineEntriesBLL.GetSingleLineEntries(null, fieldName).Select(s => s.FieldValue).ToList();

            if (addNewEntryOption)
                values.Add(Texts.NewEntry);

            return values;
        }
    }
}
