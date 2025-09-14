namespace TrainingCenterAPI.Enums
{
    public class Enums
    {
        public enum StudentStatus
        {

            Accepted, //مقبولة
            waiting,   // انتظار
            refused   //مرفوض


        }
        public enum NewStudentStatus
        {

            New,
            waiting,   // انتظار
            refused   //مرفوض


        }

        public enum evaluationOwnerType
        {

            Student,  //طالب
            Guardian   //ولي امر 

        }

    }
}
