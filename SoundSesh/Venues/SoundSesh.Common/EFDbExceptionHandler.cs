using GenericBizRunner;
using Microsoft.EntityFrameworkCore;
using System;

namespace SoundSesh.Common
{
    public static class EFDbExceptionHandler
    {
        public static IStatusGeneric SaveChangesError(Exception e, DbContext context)
        {
            var dbUpdateEx = e as DbUpdateException;
            string readableError = "";
            string propertyName = "";

            if (dbUpdateEx.InnerException != null && dbUpdateEx.InnerException.Message.Contains("Cannot insert duplicate key row"))
            {
                //TODO Is there a better way to do this? I hate string manipulation like this...
                propertyName = dbUpdateEx.InnerException?.Message.Substring(
                    dbUpdateEx.InnerException.Message.LastIndexOf("_") + 1,
                    dbUpdateEx.InnerException.Message.LastIndexOf("'") - dbUpdateEx.InnerException.Message.LastIndexOf("_") - 1);

                readableError = "Already Exists";
            }

            return new StatusGenericHandler().AddError(readableError, propertyName);
        }
    }
}
