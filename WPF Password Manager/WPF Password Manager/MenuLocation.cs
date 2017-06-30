
namespace WPF_Password_Manager
{
    public partial class MainWindow
    {
        //use byte as type to save space, only 8bits long
        public enum MenuLocation : byte { Main, Container, Box}
        private enum ErrorCode : byte {Exception, NullReferenceException, ArgumentNullException, DeleteItem}
        public enum EventType : byte { Add, Delete, Edit, ReTitle, Back, Select}
    }

    
}
