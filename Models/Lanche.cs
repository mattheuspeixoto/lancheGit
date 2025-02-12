﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage = "O Nome do Lanche deve ser informado")]
        [Display(Name = "Nome:")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {2} e no Máximo {1} Caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do lanche deve ser informada")]
        [Display(Name = "Descrição:")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição deve ter no máximo {1} caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A descrição detalhada do lanche deve ser informada")]
        [Display(Name = "Descrição detalhada:")]
        [MinLength(20, ErrorMessage = "Descrição detalhada deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição detalhada deve ter no maximo {1} caracteres")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o Preço do Lanche")]
        [Display(Name = "Preço:")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.99,ErrorMessage ="O preço deve estar entre 1 e 999,99")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho da Imagem Normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho da Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        [Display(Name = "Categoria:")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
