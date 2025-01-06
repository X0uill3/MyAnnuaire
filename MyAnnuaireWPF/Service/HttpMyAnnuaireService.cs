using MyAnnuaireModel.Dto;
using MyAnnuaireModel.Entities;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace MyAnnuaireWPF.Service
{
    internal class HttpMyAnnuaireService
    {
        private const string baseAddress = "https://localhost:7126/";
        private static HttpClient? client;
        private static CookieContainer cookieContainer = new();


        private static HttpClient Client
        {
            get
            {
                if (client == null)
                {
                    var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
                    client = new(handler) { BaseAddress = new Uri(baseAddress) };
                }
                return client;
            }
        }


        public static async Task<bool> Login(string username, string password)
        {
            string route = "login?useCookies=true&useSessionCookies=true";
            var jsonString = "{ \"email\": \"" + username + "\", \"password\": \"" + password + "\" }";

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(route, httpContent);

            var cookies = cookieContainer.GetCookies(new Uri(baseAddress));
            Debug.WriteLine(cookies);

            return response.IsSuccessStatusCode ? true :
                throw new Exception(response.ReasonPhrase);
        }

        //---------------------------------------------------------------------------------------------------//
        // Utilisateur
        // Méthode GET pour récupérer les utilisateurs
        public static async Task<List<UtilisateurDto>> GetUtilisateurs()
        {
            string route = "api/Utilisateurs";
            var response = await Client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                string resultat = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UtilisateurDto>>(resultat)
                    ?? throw new FormatException($"Erreur Http : {route}");
            }
            throw new Exception(response.ReasonPhrase);
        }

        // Méthode POST pour créer un utilisateur
        public static async Task<bool> CreateUtilisateur(UtilisateurDto utilisateur)
        {
            string route = "api/Utilisateurs";
            var jsonContent = JsonConvert.SerializeObject(utilisateur);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la création de l'utilisateur : {response.ReasonPhrase}");
            }
        }

        // Méthode PUT pour mettre à jour un utilisateur
        public static async Task<bool> UpdateUtilisateur(int id, UtilisateurDto utilisateur)
        {
            string route = $"api/Utilisateurs/{id}";
            var jsonContent = JsonConvert.SerializeObject(utilisateur);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la mise à jour de l'utilisateur : {response.ReasonPhrase}");
            }
        }

        // Méthode DELETE pour supprimer un utilisateur
        public static async Task<bool> DeleteUtilisateur(int id)
        {
            string route = $"api/Utilisateurs/{id}";
            var response = await Client.DeleteAsync(route);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la suppression de l'utilisateur : {response.ReasonPhrase}");
            }
        }

        // Méthode GET pour obtenir un utilisateur grâce à son email
        public static async Task<bool> VerifyUsernameAndPassword(string username, string password)
        {
            string route = $"api/Utilisateurs/auth/login";  // Route de votre API
            var loginRequest = new Utilisateur
            {
                login = username,
                password = password
            };

            try
            {
                // Faire un POST pour l'authentification
                var response = await Client.PostAsJsonAsync(route, loginRequest);

                // Lire le contenu de la réponse (en tant que chaîne JSON)
                string responseContent = await response.Content.ReadAsStringAsync();

                // Si la réponse est OK, l'authentification a réussi
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    // Si la réponse est une erreur, extraire le message d'erreur
                    var errorResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    string errorMessage = errorResponse?.message;

                    // Lancer l'exception avec uniquement le message d'erreur
                    return false;
                    throw new Exception(errorMessage ?? "Erreur inconnue");
                }
            }
            catch (Exception ex)
            {
                // Renvoie uniquement le message d'erreur sans préfixe
                return false;
                throw new Exception(ex.Message);
            }
        }

        //---------------------------------------------------------------------------------------------------//
        // Salariés
        // Méthode GET pour récupérer les salariés
        public static async Task<List<SalarieDto>> GetSalaries()
        {
            string route = "api/Salaries";
            var response = await Client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                string resultat = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SalarieDto>>(resultat)
                    ?? throw new FormatException($"Erreur Http : {route}");
            }
            throw new Exception(response.ReasonPhrase);
        }

        // Méthode POST pour créer un salarié
        public static async Task<bool> CreateSalarie(SalarieDto salarie)
        {
            var sal = new Salarie { Id = salarie.Id, ServiceId = salarie.ServiceId, SiegeId = salarie.SiegeId, Nom = salarie.Nom, Email = salarie.Email, TelephoneFixe = salarie.TelephoneFixe, TelephonePortable = salarie.TelephonePortable, Prenom = salarie.Prenom };

            string route = "api/Salaries";
            var jsonContent = JsonConvert.SerializeObject(sal);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la création du salarié : {response.ReasonPhrase}");
            }
        }

        // Méthode PUT pour mettre à jour un salarié
        public static async Task<bool> UpdateSalarie(int id, SalarieDto salarie)
        {
            var sal = new Salarie { Id = salarie.Id, ServiceId = salarie.ServiceId, SiegeId = salarie.SiegeId, Nom = salarie.Nom, Email = salarie.Email, TelephoneFixe = salarie.TelephoneFixe, TelephonePortable = salarie.TelephonePortable, Prenom = salarie.Prenom };

            string route = $"api/Salaries/{id}";
            var jsonContent = JsonConvert.SerializeObject(sal);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la mise à jour du salarié : {response.ReasonPhrase}");
            }
        }

        // Méthode DELETE pour supprimer un salarié
        public static async Task<bool> DeleteSalarie(int id)
        {
            string route = $"api/Salaries/{id}";
            var response = await Client.DeleteAsync(route);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la suppression du salarié : {response.ReasonPhrase}");
            }
        }
        
        // Méthode GET pour Rechercher un salarié
        public static async Task<List<SalarieDto>> RechercherSalarie(string parametre)
        {
            string route = $"api/Salaries/search/{parametre}";
            var response = await Client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                string resultat = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SalarieDto>>(resultat)
                    ?? throw new FormatException($"Erreur Http : {route}");
            }
            throw new Exception(response.ReasonPhrase);
        }

        //---------------------------------------------------------------------------------------------------//
        // Service
        // Méthode GET pour récupérer les Service
        public static async Task<List<ServiceDto>> GetServices()
        {
            string route = "api/Service";
            var response = await Client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                string resultat = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ServiceDto>>(resultat)
                    ?? throw new FormatException($"Erreur Http : {route}");
            }
            throw new Exception(response.ReasonPhrase);
        }

        // Méthode POST pour créer un Service
        public static async Task<bool> CreateService(ServiceDto salarie)
        {
            string route = "api/Service";
            var jsonContent = JsonConvert.SerializeObject(salarie);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la création du Service : {response.ReasonPhrase}");
            }
        }

        // Méthode PUT pour mettre à jour un Service
        public static async Task<bool> UpdateService(int id, ServiceDto salarie)
        {
            string route = $"api/Service/{id}";
            var jsonContent = JsonConvert.SerializeObject(salarie);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la mise à jour du Service : {response.ReasonPhrase}");
            }
        }

        // Méthode DELETE pour supprimer un Service
        public static async Task<bool> DeleteService(int id)
        {
            string route = $"api/Service/{id}";
            var response = await Client.DeleteAsync(route);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la suppression du Service : {response.ReasonPhrase}");
            }
        }

        //---------------------------------------------------------------------------------------------------//
        // Service
        // Méthode GET pour récupérer les Service
        public static async Task<List<SiegeDto>> GetSieges()
        {
            string route = "api/Siege";
            var response = await Client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                string resultat = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SiegeDto>>(resultat)
                    ?? throw new FormatException($"Erreur Http : {route}");
            }
            throw new Exception(response.ReasonPhrase);
        }

        // Méthode POST pour créer un Service
        public static async Task<bool> CreateSiege(SiegeDto salarie)
        {

            var siege = new Siege { Id = salarie.Id, VilleId = salarie.VilleId, TypeSiegeId = salarie.TypeSiegeId };

            string route = "api/Siege";
            var jsonContent = JsonConvert.SerializeObject(siege);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la création du Siege : {response.ReasonPhrase}");
            }
        }

        // Méthode PUT pour mettre à jour un Service
        public static async Task<bool> UpdateSiege(int id, SiegeDto salarie)
        {
            var siege = new Siege { Id = salarie.Id, VilleId = salarie.VilleId, TypeSiegeId = salarie.TypeSiegeId };

            string route = $"api/Siege/{id}";
            var jsonContent = JsonConvert.SerializeObject(siege);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la mise à jour du Siege : {response.ReasonPhrase}");
            }
        }

        // Méthode DELETE pour supprimer un Service
        public static async Task<bool> DeleteSiege(int id)
        {
            string route = $"api/Siege/{id}";
            var response = await Client.DeleteAsync(route);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la suppression du Siege : {response.ReasonPhrase}");
            }
        }

        //---------------------------------------------------------------------------------------------------//
        // TypeSiege
        // Méthode GET pour récupérer les TypeSiege
        public static async Task<List<TypeSiegeDto>> GetTypeSieges()
        {
            string route = "api/TypeSiege";
            var response = await Client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                string resultat = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TypeSiegeDto>>(resultat)
                    ?? throw new FormatException($"Erreur Http : {route}");
            }
            throw new Exception(response.ReasonPhrase);
        }

        // Méthode POST pour créer un TypeSiege
        public static async Task<bool> CreateTypeSiege(TypeSiegeDto salarie)
        {
            string route = "api/TypeSiege";
            var jsonContent = JsonConvert.SerializeObject(salarie);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la création du type de siège : {response.ReasonPhrase}");
            }
        }

        // Méthode PUT pour mettre à jour un Service
        public static async Task<bool> UpdateTypeSiege(int id, TypeSiegeDto salarie)
        {
            string route = $"api/TypeSiege/{id}";
            var jsonContent = JsonConvert.SerializeObject(salarie);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la mise à jour du type de siège : {response.ReasonPhrase}");
            }
        }

        // Méthode DELETE pour supprimer un Service
        public static async Task<bool> DeleteTypeSiege(int id)
        {
            string route = $"api/TypeSiege/{id}";
            var response = await Client.DeleteAsync(route);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la suppression du Service : {response.ReasonPhrase}");
            }
        }

        //---------------------------------------------------------------------------------------------------//
        // Pays
        // Méthode GET pour récupérer les Pays
        public static async Task<List<PaysDto>> GetPays()
        {
            string route = "api/Pays";
            var response = await Client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                string resultat = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PaysDto>>(resultat)
                    ?? throw new FormatException($"Erreur Http : {route}");
            }
            throw new Exception(response.ReasonPhrase);
        }

        // Méthode POST pour créer un Pays
        public static async Task<bool> CreatePays(PaysDto salarie)
        {
            string route = "api/Pays";
            var jsonContent = JsonConvert.SerializeObject(salarie);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la création du pays : {response.ReasonPhrase}");
            }
        }

        // Méthode PUT pour mettre à jour un Pays
        public static async Task<bool> UpdatePays(int id, PaysDto salarie)
        {
            string route = $"api/Pays/{id}";
            var jsonContent = JsonConvert.SerializeObject(salarie);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la mise à jour du pays : {response.ReasonPhrase}");
            }
        }

        // Méthode DELETE pour supprimer un Pays
        public static async Task<bool> DeletePays(int id)
        {
            string route = $"api/Pays/{id}";
            var response = await Client.DeleteAsync(route);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la suppression du pays : {response.ReasonPhrase}");
            }
        }

        //---------------------------------------------------------------------------------------------------//
        // Ville
        // Méthode GET pour récupérer les Villes
        public static async Task<List<VilleDto>> GetVilles()
        {
            string route = "api/Ville";
            var response = await Client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                string resultat = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<VilleDto>>(resultat)
                    ?? throw new FormatException($"Erreur Http : {route}");
            }
            throw new Exception(response.ReasonPhrase);
        }

        // Méthode POST pour créer un Ville
        public static async Task<bool> CreateVille(VilleDto salarie)
        {
            var ville = new Ville { Id = salarie.Id, PaysId = salarie.PaysId, Name = salarie.Name };

            string route = "api/Ville";
            var jsonContent = JsonConvert.SerializeObject(ville);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la création de la ville : {response.ReasonPhrase}");
            }
        }

        // Méthode PUT pour mettre à jour un Ville
        public static async Task<bool> UpdateVille(int id, VilleDto salarie)
        {
            var ville = new Ville { Id = salarie.Id, PaysId = salarie.PaysId, Name=salarie.Name };

            string route = $"api/Ville/{id}";
            var jsonContent = JsonConvert.SerializeObject(ville);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(route, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la mise à jour de la ville : {response.ReasonPhrase}");
            }
        }

        // Méthode DELETE pour supprimer un Ville
        public static async Task<bool> DeleteVille(int id)
        {
            string route = $"api/Ville/{id}";
            var response = await Client.DeleteAsync(route);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la suppression de la ville : {response.ReasonPhrase}");
            }
        }
    }
}
