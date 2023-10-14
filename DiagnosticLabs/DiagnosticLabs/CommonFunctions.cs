using DiagnosticLabs.Constants;
using DiagnosticLabs.Models;
using DiagnosticLabsBLL.Globals;
using DiagnosticLabsBLL.Services;
using DiagnosticLabsDAL.Models;
using DiagnosticLabsDAL.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DiagnosticLabs
{
    public class CommonFunctions
    {
        SingleLineEntriesBLL _singleLineEntriesBLL = new SingleLineEntriesBLL();
        CompaniesBLL _companiesBLL = new CompaniesBLL();
        PackagesBLL _packagesBLL = new PackagesBLL();
        PatientRegistrationsBLL _patientRegistrationsBLL = new PatientRegistrationsBLL();

        #region Common
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
            List<string> values = _singleLineEntriesBLL.GetSingleLineEntries(null, fieldName).Select(s => s.FieldValue).ToList();

            if (addNewEntryOption)
                values.Add(Texts.NewEntry);

            return values;
        }

        public List<Company> CompaniesList(bool includeSystemRecord = false, bool includeAllSelection = false)
        {
            List<Company> companies = new List<Company>();
            companies = _companiesBLL.GetAllCompanies(includeSystemRecord);

            if (includeAllSelection)
                companies.Insert(0, new Company() { Id = -1, CompanyName = "--ALL--", Address = "--ALL--", ContactNumbers = "--ALL--", ContactPerson = "--ALL--", IsActive = true });

            return companies;
        }

        public List<Package> PackagesList(bool addNone = false)
        {
            List<Package> packages = _packagesBLL.GetAllPackages();

            if (addNone)
                packages.Insert(0, new Package() { Id = 0, PackageName = "--NONE--" });

            return packages;
        }

        public List<PatientRegistrationBatch> PatientRegistrationBatchList(long? companyId)
        {
            return _patientRegistrationsBLL.GetPatientRegistrationBatches(companyId);
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

        public int GetModuleId(string moduleName)
        {
            return Globals.MODULES.Where(m => m.ModuleName == moduleName).Select(m => m.Id).FirstOrDefault();
        }

        #endregion

        #region Lab Results
        public List<string> LabResultsGeneralSingleLineEntryList(string fieldName, bool addNewEntryOption = false)
        {
            List<string> values = _singleLineEntriesBLL.GetSingleLineEntries(null, fieldName).Select(s => s.FieldValue).ToList();

            if (addNewEntryOption)
                values.Add(Texts.NewEntry);

            return values;
        }

        public List<string> LabResultsSingleLineEntryList(string fieldName, int? moduleId, bool addNewEntryOption = false)
        {
            List<string> values = _singleLineEntriesBLL.GetSingleLineEntries(moduleId, fieldName).Select(s => s.FieldValue).ToList();

            if (addNewEntryOption)
                values.Add(Texts.NewEntry);

            return values;
        }
        #endregion
    }
}
