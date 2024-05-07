namespace CRUDTS5DANAMISE
{
    public partial class App : Application
    {
        public static PersonRepository PersonRepo {  get; set; }
        public App(PersonRepository personRepository)
        {
            InitializeComponent();

            MainPage = new Views.vPerson();
            PersonRepo = personRepository;
        }
    }
}
