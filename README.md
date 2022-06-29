[<img src="https://s3.amazonaws.com/usdphosting.accusoft-ontask/wp-content/uploads/Ontask-API-Logo-2.png.webp" width="600"/>](https://www.ontask.io/solutions/ontask-api/)

# OnTask eSignature API .NET Sample

## Requirements

- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) or higher

- You will first need to [sign up for an API developer account](https://app.ontask.io/signup?source=eSigApi) with OnTask and retrieve your API key.

- Within the code of `Program.cs` in the root folder insert the API key you received after creating your account in the `apiKey` variable within each page.

- Within the code of `Program.cs` update the `emailSigner` and `emailFinalized` variables to the emails you wish to test with.

## Sample

This sample configures a signature ceremony for two users:

- John Smith for whom the system will return a hyperlink that can be presented in your solution for the user to complete.  

- Jane Smith who will receive an email that is configured with the `emailSigner` variable at the top of the `Program.cs` file in the root of the project.

Once both signers have completed the signing ceremony the completed document will be sent to the email configured with the `emailFinalized` variable at the top of the `Program.cs` file.

Use `dotnet run` to execute the sample.

## About OnTask

Building secure, compliant eSignature capabilities into your application takes up valuable time and development resources. Fortunately, we've already done all that hard work for you. With [OnTask API](https://www.ontask.io/solutions/ontask-api/), you can bridge the gaps between systems by integrating our proven eSignature and forms workflow functionality into your software. OnTask is reliable and secure; and takes the guesswork out of compliance by meeting regulations for HIPAA, SOC 2, and more. 

We've got all the documentation samples you need to get up and running within a matter of minutes so you can spend more time working on the innovative features that will make your application a success.

Choose from a library of templates or create your own workflows using flexible conditional logic. OnTask's intuitive, easy-to-navigate interface allows you to start solving problems right away without weeks of tedious onboarding and complex integrations.
