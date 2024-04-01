namespace Gerenciador_de_Tarefas.Services;

public class PasswordVerificationService
{
    public static bool CheckPassword(string hashedPassword, string dbHashedPassword)
    {
        return hashedPassword.Equals(dbHashedPassword);
    }

}
