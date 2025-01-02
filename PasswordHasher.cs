using System.Security.Cryptography;

namespace IfoodParaguai.PassHash;
class CriptografarSenha
{
    // Método para gerar um salt aleatório
    public string GerarSalt()
    {
        byte[] saltBytes = new byte[16]; // Salt de 16 bytes
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    // Método para gerar o hash da senha com o salt
    public string GerarHash(string senha, string salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(senha, Convert.FromBase64String(salt), 10000))
        {
            byte[] hashBytes = pbkdf2.GetBytes(20); // 20 bytes é um tamanho comum para o hash
            return Convert.ToBase64String(hashBytes);
        }
    }

    // Método para verificar se a senha fornecida é válida
    public bool VerificarSenha(string senha, string salt, string hashExistente)
    {
        Console.WriteLine(salt);
        Console.WriteLine(senha);
        Console.WriteLine(hashExistente);
        string hashNova = GerarHash(senha, salt);
        return hashNova == hashExistente;
    }
}