using azure_translator;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class TranslatorController : ControllerBase
{
    private readonly Translator _translator;

    public TranslatorController()
    {
        _translator = new Translator();
    }

    [HttpGet]
    public Translator GetTranslation(string curText = "", string language)
    {
        string endpoint = "<>"; // отобрали акк
        string apiKey = "<>";  // отобрали акк

        _translator.CurrentText = curText;

        TranslationDocumentClient client = new TranslationDocumentClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

        string sourceLanguage = "<>"; 
        string targetLanguage = language;

        TranslationDocumentInput input = new TranslationDocumentInput(curText);

        TranslationBatch batch = new TranslationBatch(new TranslationDocumentInput[] { input });

        TranslationOperation operation = client.StartTranslationBatch(batch, sourceLanguage, new string[] { targetLanguage });

        Response<TranslationResultCollection> response = operation.WaitForCompletionAsync().GetAwaiter().GetResult();

        TranslationResultCollection result = response.Value;

        string translation = result[0].Translations[0].Text;

        _translator.TranslationResult = translation;

        return _translator;
    }

}