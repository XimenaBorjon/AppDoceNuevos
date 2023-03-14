using System;
using AppDoceNuevos.Models;
using AppDoceNuevos.Services;
using AppDoceNuevos.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppDoceNuevos.ViewModels
{
    public class DirectorViewModels : INotifyPropertyChanged
    {
        public ObservableCollection<Docente> ListaDocente { get; set; } = new ObservableCollection<Docente>();
        public ObservableCollection<Usuario> ListaUsuario { get; set; } = new ObservableCollection<Usuario>();

        public ObservableCollection<Grupo> Grupolist { get; set; } = new ObservableCollection<Grupo>();
        public ObservableCollection<Asignatura> AsignaturaList { get; set; } = new ObservableCollection<Asignatura>();
        public ObservableCollection<Periodo> PeriodoList { get; set; } = new ObservableCollection<Periodo>();
        public ObservableCollection<DocenteGrupo> DocenteGrupoList { get; set; } = new ObservableCollection<DocenteGrupo>();
        public ObservableCollection<DocenteAsignatura> DocenteAsignaturaList { get; set; } = new ObservableCollection<DocenteAsignatura>();
        public DocenteGrupo docgrupo { get; set; } = new DocenteGrupo();

        AggDocente VistaAgregar;
       
        GruposActuales VistaGrupos;

        AggUsuario VistaAggUsuario;
        EditarUsuario VistaEdiUsuario;
        UsuariosActuales VistaUsuActuales;


        DirectorServices servi = new();


        public Command AgregarCommand { get; set; }
        public Command EditarCommand { get; set; }
        public Command LoginCommand { get; set; }
        public Command EliminarCommand { get; set; }
        public Command VerEditarCommand { get; set; }
        public Command VerGruposCommand { get; set; }
        public Command VerAgregarCommand { get; set; }
        public Command AgregarUsuarioCommand { get; set; }
        public Command EditarUsuarioCommand { get; set; }
        public Command EliminarUsuarioCommand { get; set; }
        public Command VerUsuariosCommand { get; set; }
        public Command VerAgregarUsuCommand { get; set; }
        public Command VerEditUsuCommand { get; set; }
        public Command VerUsuActualesCommand { get; set; }

        public Docente docente { get; set; } = new Docente();
        public Usuario usuario { get; set; } = new Usuario();
        public string Error { get; set; } = "";

        public DirectorViewModels()
        {
            AgregarCommand = new Command(Agregar);
            EditarCommand = new Command(Editar);
            EliminarCommand = new Command(Eliminar);
            LoginCommand = new Command(Login);
            VerAgregarCommand = new Command(VerAgregar);
            VerEditarCommand = new Command<Docente>(VerEditar);
            VerGruposCommand = new Command(VerGrupos);

            AgregarUsuarioCommand = new Command(AgregarUsuario);
            EditarUsuarioCommand = new Command(EditarUsuarios);
            EliminarUsuarioCommand = new Command(EliminarUsuario);
            VerUsuariosCommand = new Command(VerUsuarioActuales);
            VerAgregarUsuCommand = new Command(VerAgregarUsuarios);
            VerEditUsuCommand = new Command<Usuario>(VerEditarUsuario);

            CargarDocente();
            CargarUsuarios();
        }

        private async void VerEditarUsuario(Usuario u)
        {
            Error = "";
            Actualizar(nameof(Error));
            usuario = new Usuario
            {
                Id = u.Id,
                Usuario1 = u.Usuario1,
                Contraseña = u.Contraseña,
                Rol = u.Rol
            };
            VistaEdiUsuario = new EditarUsuario() { BindingContext = this };
            await Application.Current.MainPage.Navigation.PushAsync(VistaEdiUsuario);
        }

        private async void VerAgregarUsuarios()
        {
            VistaAggUsuario = new AggUsuario() { BindingContext = this };
            await Application.Current.MainPage.Navigation.PushAsync(VistaAggUsuario);
        }

        private async void VerUsuarioActuales()
        {
            VistaUsuActuales = new UsuariosActuales() { BindingContext = this };
            await Application.Current.MainPage.Navigation.PushAsync(VistaUsuActuales);
        }

        private async void EliminarUsuario()
        {
            ListaUsuario.Remove(usuario);
            Actualizar(nameof(ListaUsuario));
            await servi.DeleteUsuario(usuario);
            CargarUsuarios();
            
        }

        private async void EditarUsuarios()
        {
            await servi.UpdateUsuario(usuario);
            await Application.Current.MainPage.Navigation.PopAsync();
            CargarUsuarios();
        }

        private async void AgregarUsuario()
        {
            usuario.Rol = 2;
            ListaUsuario.Add(usuario);
            Actualizar(nameof(ListaUsuario));
            await servi.InsertUsuario(usuario);
            
        }

        private async void Login()
        {
            Error = "";
            if (usuario != null)
            {
                if (await servi.Login(usuario))
                {
                    App.Current.MainPage = new NavigationPage(new UsuariosActuales());
                }
            }
            Actualizar(nameof(Error));
        }

        async void CargarDocente()
        {
            ListaDocente.Clear();
            var datos = await servi.GetDocentes();
            datos.ForEach(x => ListaDocente.Add(x));
            Actualizar(nameof(ListaDocente));
        }

        async void CargarUsuarios()
        {
            ListaUsuario.Clear();
            var datos = await servi.GetUsuarios();
            datos.ForEach(x => ListaUsuario.Add(x));
            Actualizar(nameof(ListaUsuario));
        }

        private void VerGrupos()
        {
            VistaGrupos = new GruposActuales() { BindingContext = this };
            Application.Current.MainPage.Navigation.PushAsync(VistaGrupos);

        }

        EditarDocente VistaEditar;
        private async void VerEditar(Docente d)
        {
            docente = d;
            docente.Nombre = d.Nombre;
            docente.ApellidoMaterno = d.ApellidoMaterno;
            docente.ApellidoPaterno = d.ApellidoPaterno;
            docente.Correo = d.Correo;
            docente.Telefono = d.Telefono;
            docente.Edad = d.Edad;
            docente.TipoDocente = d.TipoDocente;
            docente.IdUsuario = d.IdUsuario;
           
            VistaEditar = new EditarDocente() { BindingContext = this };
            await Application.Current.MainPage.Navigation.PushAsync(VistaEditar);

        }

        private async void VerAgregar()
        {
            VistaAgregar = new AggDocente() { BindingContext = this };
            await Application.Current.MainPage.Navigation.PushAsync(VistaAgregar);
        }

        private async void Agregar()
        {
            docente.IdUsuario = usuario.Id;
            ListaDocente.Add(docente);
            Actualizar(nameof(ListaDocente));
            await servi.AgregarDocente(docente);
        }

        private async void Editar()
        {
            await servi.Update(docente);
            await Application.Current.MainPage.Navigation.PopAsync();
            CargarDocente();

        }

        private async void Eliminar()
        {
            ListaDocente.Remove(docente);
            Actualizar(nameof(ListaDocente));
            await servi.Delete(docente);
            CargarDocente();
        }

        void Actualizar(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
