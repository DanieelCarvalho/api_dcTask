namespace Gerenciador_de_Tarefas.Domain.Models;

public class Endereco : Entity
{

    public string Rua { get; set; }
    public string Cep { get; set; }
    public string Estado { get; set; }

    public string Cidade { get; set; }


}
