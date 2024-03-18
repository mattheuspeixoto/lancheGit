using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LanchesMac.TagHelpers;

public class TelefoneTagHelper : TagHelper {
    public string Endereco { get; set; }
    public string Conteudo { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "a";
        output.Attributes.SetAttribute("href", "tel:+55" + Endereco);
        output.Content.SetContent(Conteudo);
    }
}

