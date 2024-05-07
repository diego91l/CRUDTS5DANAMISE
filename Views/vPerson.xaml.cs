using CRUDTS5DANAMISE.Models;

namespace CRUDTS5DANAMISE.Views;

public partial class vPerson : ContentPage
{

	public vPerson()
	{
		InitializeComponent();
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        App.PersonRepo.AddNewPerson(txtPersona.Text);
        lblStatus.Text = App.PersonRepo.StatusMessage;
        txtPersona.Text = ""; // Limpiar el campo de entrada después de agregar
    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        List<Person> people = App.PersonRepo.GetAllPeople();
        litsView.ItemsSource = people;
        
    }

          private async void EditPerson(Person person)
        {
            string newName = await DisplayPromptAsync("Editar Persona", "Ingrese el nuevo nombre:", "Save", "Cancel", null, -1, Keyboard.Default, person.Name);
            if (newName != null)
            {
                person.Name = newName;
                App.PersonRepo.UpdatePerson(person.Id, newName);
                btnObtener_Clicked(this, EventArgs.Empty); // Volver a cargar la lista de personas
            }
        }

        private async void DeletePerson(Person person)
        {
            bool answer = await DisplayAlert("Confirma la eliminacion", $"Estas seguro que quieres eliminar {person.Name}?", "Si", "No");
            if (answer)
            {
                App.PersonRepo.DeletePerson(person.Id);
                btnObtener_Clicked(this, EventArgs.Empty); // Volver a cargar la lista de personas
            }
        }


    private async void litsView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var person = (Person)e.Item;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
        
        switch (action) 
        {
            case "Edit":
                EditPerson(person);
                break;

            case "Delete":
                DeletePerson(person);
                break;
        }

    }
}