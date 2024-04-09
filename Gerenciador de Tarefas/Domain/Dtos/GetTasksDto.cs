namespace Gerenciador_de_Tarefas.Domain.Dtos;
    public class GetTasksDto
    {

  
    public string tarefa { get; set; }
   
    public DateTime DataInitio { get; set; }
   
    public DateTime DataFim { get; set; }
  
    public string Descricao { get; set; }
    public string UserId { get; set; }
    public int Id { get; set; }
    public string status { get; set; }

}

