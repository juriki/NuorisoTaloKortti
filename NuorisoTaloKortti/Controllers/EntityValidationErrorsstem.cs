using System;
using System.Runtime.Serialization;

namespace NuorisoTaloKortti.Controllers
{
    [Serializable]
    internal class EntityValidationErrorsstem : Exception
    {
        public EntityValidationErrorsstem()
        {
        }

        public EntityValidationErrorsstem(string message) : base(message)
        {
        }

        public EntityValidationErrorsstem(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityValidationErrorsstem(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}