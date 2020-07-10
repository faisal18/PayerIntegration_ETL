using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayerIntegration_ETL.Helpers
{
    class DBQueries
    {
        public static string PriorRequestIDs()
        {
            string sqlQuery = @"Select top 100 at.id  from 
            AUTHORIZATION_TRANSACTION at  with (Nolock) 
                inner join PRIOR_REQUEST PR  with (Nolock) on At.id = pr.id
                where 
                    -- at.payer_id = @Payerid
                -- and 
                PR.download_batch_id in (null, '0')
                and at.state_id IN (3,4,8)
                and at.created_dt  < DATEADD(HOUR, -1, GETDATE())";

            return sqlQuery;
        }



        public static string PriorRequestTransactionbyID (string Transid)
        {
//            string sqlQuery = @"Select * from " 
//+ " PBMM..AUTHORIZATION_TRANSACTION at  with (Nolock) "
//+ " inner join PBMM..PRIOR_REQUEST PR  with (Nolock) on At.id = pr.id "  
//+ " inner join PBMM..[PROVIDER] P with (nolock) on P.id = at.provider_id "
//+ " inner join PBMM..PRIOR_REQUEST_HEADER PRH  with (Nolock) on PRH.prior_request_id = PR.id "
//+ " inner join PBMM..PRIOR_REQUEST_ENCOUNTER PRE  with (Nolock) on PRE.Prior_Request_Id = PR.ID "
//+ " inner join PBMM..PRIOR_REQUEST_DIAGNOSIS PRD  with (Nolock) on PRD.prior_request_id = PR.ID "
//+ " inner join PBMM..PRIOR_REQUEST_ACTIVITY PRA  with (Nolock) on PRA.prior_Request_id = PR.ID "
//+ " inner join PBMM..PRIOR_REQUEST_ACT_OBSERVATION PRAO  with (Nolock) on PRAO.request_id = PR.ID  and PRA.id = PRAO.Activity_ID  " 
//+ " left join PBMM..PRIOR_REQUEST_RESUBMISSION PRR  with (Nolock) on PRR.request_id = PR.id "
//+ " left join PBMM..POST_OFFICE_COMM POC  with (Nolock) on POC.Trans_ID = AT.id "
//+ "  where at.id = "  + Transid;


            string sqlQuery = @"Select " +
"--HEADER--\n " +
"PRH.sender_ID as 'HeadSenderID', " +
"PRH.receiver_ID as 'HeadReceiverID', " +
"PRH.transaction_date as 'HeadTransactionDate', " +
"PRH.record_count as 'HeadRecordCount', " +
"PRH.disposition_flag as 'HeadDispositionFlag', " +
"--AUTHORIZAITION--\n " +
"pr.type as 'AuthType', " +
"at.request_id as 'AuthID', " +
"at.member_id as 'AuthMemberID', " +
"at.payer_id as 'AuthPayerID', " +
"pr.emirates_id as 'AuthEmiratesIDNumber',  " +
"pr.date_ordered as 'AuthDateOrdered', " +
"--ENCOUNTER--\n" +
"PRE.facility_ID as 'EncounterFacilityID', " +
"PRE.type as 'EncounterType', " +
"--DIAGNOSIS -- \n" +
"PRD.type as 'DiagnosisType', " +
"PRD.code as 'DiagnosisCode', " +
" --ACTIVITY--\n" +
"'Activity' as 'Activity',"+
"PRA.activity_ID as 'AcitivityId', " +
"PRA.start_date as 'ActivityStartDate', " +
"PRA.type as 'ActivityType', " +
"PRA.code as 'ActivityCode', " +
"PRA.quantity as 'ActivityQunatity', " +
"PRA.net as 'ActivityNet', " +
"PRA.clinician as 'ActivityClinician', " +
"--OBSERVATION--\n" +
"PRAO.type as 'ObservationType', " +
"PRAO.code as 'ObservationCode', " +
"PRAO.value as 'ObservationValue', " +
"PRAO.value_type as 'ObservationValueType', " +
"--RESUBMISSION--\n" +
"PRR.type as 'ResubType', " +
"PRR.comment as 'ResubComment', " +
"PRR.attachment as 'ResubAttach' " +
"--,* \n" +
"from   " +
"PBMM..AUTHORIZATION_TRANSACTION at  with (Nolock)   " +
"inner join PBMM..PRIOR_REQUEST PR  with (Nolock) on At.id = pr.id   " +
"inner join PBMM..[PROVIDER] P with (nolock) on P.id = at.provider_id   " +
"inner join PBMM..PRIOR_REQUEST_HEADER PRH  with (Nolock) on PRH.prior_request_id = PR.id   " +
"inner join PBMM..PRIOR_REQUEST_ENCOUNTER PRE  with (Nolock) on PRE.Prior_Request_Id = PR.ID   " +
"inner join PBMM..PRIOR_REQUEST_DIAGNOSIS PRD  with (Nolock) on PRD.prior_request_id = PR.ID   " +
"inner join PBMM..PRIOR_REQUEST_ACTIVITY PRA  with (Nolock) on PRA.prior_Request_id = PR.ID   " +
"inner join PBMM..PRIOR_REQUEST_ACT_OBSERVATION PRAO  with (Nolock) on PRAO.request_id = PR.ID  and PRA.id = PRAO.Activity_ID    " +
"left join PBMM..PRIOR_REQUEST_RESUBMISSION PRR  with (Nolock) on PRR.request_id = PR.id   " +
"left join PBMM..POST_OFFICE_COMM POC  with (Nolock) on POC.Trans_ID = AT.id    " +
"where at.id = " + Transid;

            return sqlQuery;


        }


    }
}
