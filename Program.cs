// Get your free API Key at: https://www.ontask.io/solutions/ontask-api/
// Full OnTask API Documentation: https://docs.ontask.io/#overview
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

String apiKey = "INSERT-API-KEY";
String emailSigner = "signer@example.com";
String emailFinalized = "completed@example.com";

try
{
    Console.WriteLine("##### Example Starting #####");

    // Uploads sample file in /sample-files to the Ontask API
    String documentID = await UploadFile();
    Console.WriteLine("\nUploaded sample file and received a documentId of: " + documentID + "\n");

    // Pass documentID returned from the API to the /Signature endpoint
    String result = await StartSignatureRequest(documentID);
    JsonDocument signatureResponse = JsonDocument.Parse(result);

    // Retrieve URL for John Smith who is using the "link" option for signing
    Console.WriteLine("URL for signer John Smith: (Follow URL to complete signature process)");
    Console.WriteLine(signatureResponse.RootElement.GetProperty("signers")[0].GetProperty("contactMethod")[0].GetProperty("taskUrl").ToString() + "\n");

    // Jane Smith will receive an email which is configured in the 'emailSigner' variable above
    Console.Write("Signature request sent for Jane Smith, please check email at: ");
    Console.Write(signatureResponse.RootElement.GetProperty("signers")[1].GetProperty("contactMethod")[0].GetProperty("email").ToString() + "\n\n");

    Console.WriteLine("Once signed the final document will be sent to: " + emailFinalized + "\n");
    Console.WriteLine("##### Example Complete #####\n");
}
catch (Exception ex)
{
    Console.WriteLine("There was an error making signature request, please check your API key.");
    Console.WriteLine("Error: " + ex.Message);
}

async Task<String> UploadFile()
{
    // Upload the file and get a documentid to pass to create a signature endpoint
    // https://docs.ontask.io/#upload

    String uploadURI = "https://app.ontask.io/api/v2/documents";

    using (HttpClient client = new HttpClient())
    {
        try
        {
            client.DefaultRequestHeaders.Add("Authorization", apiKey);

            byte[] file = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/sample-files/sample.pdf");

            ByteArrayContent content = new ByteArrayContent(file);
            content.Headers.Add("Content-Type", "application/pdf");

            HttpResponseMessage result = client.PostAsync(uploadURI.ToString(), content).Result;
            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(response);

            return doc.RootElement.GetProperty("documentId").ToString();
        }
        catch (Exception)
        {
            Console.WriteLine("Error uploading file.");
            throw;
        }
    }
}

async Task<String> StartSignatureRequest(String documentID)
{
    // Call signature to and create and start signature process
    // https://docs.ontask.io/#signature-api

    String uploadURI = "https://app.ontask.io/api/v2/signatures";

    // Build JSON request for creating an signature request
    JsonObject requestBody = new JsonObject
    {
        ["documents"] = new JsonArray {
            {
                new JsonObject
                {
                    ["documentId"] = documentID
                }
            }
        },
        ["testMode"] = true,
        ["signers"] = new JsonArray
        {
            new JsonObject
            {
                ["label"] = "John Smith",
                ["contactMethod"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["type"] = "link"
                    }
                }
            },
            new JsonObject
            {
                ["label"] = "Jane Smith",
                ["contactMethod"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["type"] = "email",
                        ["email"] = emailSigner
                    }
                }
            }
        },
        ["onSignaturesFinalized"] = new JsonArray
        {
            new JsonObject
            {
                 ["type"] = "email",
                 ["email"] = emailFinalized
            }
        }
    };

    using (HttpClient client = new HttpClient())
    {
        try
        {
            client.DefaultRequestHeaders.Add("Authorization", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uploadURI, data);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("Error starting signature");
            throw;
        }
    }
}