using DiagnosticLabs.Constants;
using DiagnosticLabs.Models;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DiagnosticLabs
{
    public class CommonFunctions
    {
        SingleLineEntriesBLL singleLineEntriesBLL = new SingleLineEntriesBLL();
        CompaniesBLL companiesBLL = new CompaniesBLL();
        PackagesBLL packagesBLL = new PackagesBLL();
        PatientRegistrationsBLL patientRegistrationsBLL = new PatientRegistrationsBLL();

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

        public List<Company> CompaniesList(bool includeSystemRecord = false, bool includeAllSelection = false)
        {
            List<Company> companies = new List<Company>();
            companies = companiesBLL.GetAllCompanies(includeSystemRecord);

            if (includeAllSelection)
                companies.Insert(0, new Company() { Id = -1, CompanyName = "ALL", Address = "ALL", ContactNumbers = "ALL", ContactPerson = "ALL", IsActive = true });

            return companies;
        }

        public List<Package> PackagesList(bool addNone = false)
        {
            List<Package> packages = packagesBLL.GetAllPackages();

            if (addNone)
                packages.Insert(0, new Package() { Id = 0, PackageName = "None" });

            return packages;
        }

        public List<PatientRegistrationBatch> PatientRegistrationBatchList(long? companyId)
        {
            return patientRegistrationsBLL.GetPatientRegistrationBatches(companyId);
        }

        public NotificationMessage CustomNotificationMessage(string message, Messages.MessageType messageType, bool isAutoCloseMessage)
        {
            return new NotificationMessage()
            {
                Message = message,
                MessageType = messageType,
                IsAutoCloseMessage = isAutoCloseMessage
            };
        }
    }
}
