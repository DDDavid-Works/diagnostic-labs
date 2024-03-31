using DiagnosticLabsDAL.DatabaseContext;
using DiagnosticLabsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticLabsBLL.Services
{
    public class PatientRegistrationServicesBLL
    {
        private const string _logFileName = "PatientRegistrationServicesBLL";

        CommonFunctions _commonFunctions = new CommonFunctions();

        private static DatabaseContext _dbContext;

        public PatientRegistrationServicesBLL()
        {
            _dbContext = new DatabaseContext();
        }

        public List<PatientRegistrationService> GetPatientRegistrationServicesByPatientRegistrationId(long patientRegistrationId)
        {
            try
            {
                return _dbContext.PatientRegistrationServices.Where(p => p.PatientRegistrationId == patientRegistrationId && p.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return null;
            }
        }

        public bool SavePatientRegistrationService(PatientRegistrationService patientRegistrationService, ref long id)
        {
            try
            {
                if (patientRegistrationService.Id == 0)
                {
                    patientRegistrationService.CreatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patientRegistrationService.CreatedDate = DateTime.Now;
                    patientRegistrationService.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patientRegistrationService.UpdatedDate = DateTime.Now;
                    _dbContext.PatientRegistrationServices.Add(patientRegistrationService);
                }
                else
                {
                    patientRegistrationService.UpdatedByUserId = Globals.Globals.LOGGEDINUSERID;
                    patientRegistrationService.UpdatedDate = DateTime.Now;
                }
                _dbContext.SaveChanges();

                id = patientRegistrationService.Id;

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }

        public bool SavePatientRegistrationServiceList(List<PatientRegistrationService> patientRegistrationServices, long patientRegistrationId)
        {
            try
            {
                List<PatientRegistrationService> existingPatientRegistrationServices = GetPatientRegistrationServicesByPatientRegistrationId(patientRegistrationId);
                List<long> existingPatientRegistrationsIds = existingPatientRegistrationServices.Select(p => p.Id).ToList();
                List<PatientRegistrationService> patientRegistrationServicesToRemove = existingPatientRegistrationServices
                    .Where(p => !patientRegistrationServices.Select(ps => ps.Id).Contains(p.Id) && p.Id != 0).ToList();
                foreach (PatientRegistrationService patientRegistrationService in patientRegistrationServicesToRemove)
                {
                    long patientRegistrationServiceId = 0;
                    patientRegistrationService.IsActive = false;
                    SavePatientRegistrationService(patientRegistrationService, ref patientRegistrationServiceId);
                }

                foreach (PatientRegistrationService patientRegistrationService in patientRegistrationServices)
                {
                    if (patientRegistrationService.PatientRegistrationId == 0)
                        patientRegistrationService.PatientRegistrationId = patientRegistrationId;

                    long patientRegistrationServiceId = 0;
                    patientRegistrationService.Price = Convert.ToDecimal(_commonFunctions.NumbericValue(patientRegistrationService.PatientRegistrationServicePrice));

                    SavePatientRegistrationService(patientRegistrationService, ref patientRegistrationServiceId);
                }

                return true;
            }
            catch (Exception ex)
            {
                _commonFunctions.LogException(_logFileName, ex);
                return false;
            }
        }
    }
}
