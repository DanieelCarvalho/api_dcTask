﻿using System.ComponentModel.DataAnnotations;

namespace Gerenciador_de_Tarefas.Domain.Dtos;

public class TaskDto
{
   // [Required]
    public string tarefa { get; set; }
//[Required]
    public DateTime DataInitio { get; set; }
  //  [Required]
    public DateTime DataFim { get; set; }
    //  [Required]
    

    public string Descricao { get; set; }


}
