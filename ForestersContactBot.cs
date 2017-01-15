using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SujithBot
{


    public enum HelpOptions
    {
        HowToApplyClaim, WhatIsMyCertificateStatus, NeedForms
    };

    public enum FormsOptions
    {
        Form1, Form2, Form3, Form4, Form5, Form6, Form7, Other
    };

    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "I do not understand \"{0}\".", "Try again, I don't get \"{0}\".")]
    [Template(TemplateUsage.EnumSelectOne, "What kind of {&} would you like select? {||}")]
    public class ForestersContactBot
    {
        [Prompt("For Verification, What is your Firstname?")]
        public string FirstName;

        // [Prompt("What kind of {&} would you like? {||}")]
        // [Describe(Image = @"https://placeholdit.imgix.net/~text?txtsize=16&txt=Sandwich&w=125&h=40&txttrack=0&txtclr=000&txtfont=bold")]
        [Prompt("What kind of {&} would you like? {||}", ChoiceFormat = "{1}")]
        // [Prompt("What kind of {&} would you like?")]
        public HelpOptions? helpOptions;

        // [Pattern(@"(Undefined control sequence \d)?\s*\d{3}(-|\s*)\d{4}")]
        [Prompt("What is your Certificate Number?")]
        public string CertNumber;

        //   [Pattern(@"(Undefined control sequence \d)?\s*\d{3}(-|\s*)\d{4}")]
        [Prompt("What is your Phone Number?")]
        public string PhoneNumber;

        [Optional]
        [Template(TemplateUsage.StatusFormat, "{&}: {:t}", FieldCase = CaseNormalization.None)]
        public DateTime? DeliveryTime;

        //public List<FormsOptions> FormLists { get; set; }

        [Optional]
        // [Template(TemplateUsage.NoPreference, "None")]
        public string Requests;

        public static IForm<ForestersContactBot> BuildForm()
        {

            OnCompletionAsyncDelegate<ForestersContactBot> processRequest = async (context, state) =>
            {
                //await context.PostAsync(");
                string wrapUpMessage = "Hi " + state.FirstName + ". We are currently processing your request.We will message you the status. Thank you for your information.";
                IMessageActivity activity = context.MakeMessage();
                activity.Text = wrapUpMessage;
                await context.PostAsync(activity);


            };



            return new FormBuilder<ForestersContactBot>().Message("Hi There, Welcome to Foresters Financial Insurance Bot.")
                .Field(nameof(FirstName))
                .Field(nameof(PhoneNumber))
                .Field(nameof(CertNumber))
                .Field(nameof(helpOptions))
                .Field(new FieldReflector<ForestersContactBot>(nameof(Requests))
                             .SetType(null)
                             .SetActive((state) => state.helpOptions == HelpOptions.NeedForms)
                                .SetDefine(async (state, field) =>
                                {
                                    field
                                        .AddDescription("Form1", "Claim Form")
                                        .AddTerms("Form1", "Form1", "Claim Form")
                                        .AddDescription("Form2", "Surrender Form")
                                        .AddTerms("Form2", "Form2", "Surrender Form")
                                        .AddDescription("Form3", "Form3")
                                        .AddTerms("Form3", "Form3", "Form3")
                                        .AddDescription("Form4", "Form4")
                                        .AddTerms("Form4", "Form4", "Form4")
                                        .AddDescription("Form5", "Form5")
                                        .AddTerms("Form5", "Form5", "Form5");
                                    return true;
                                }))
                           .Field(nameof(ForestersContactBot.DeliveryTime), "When do you want your information delivered? {||}")
                        .Confirm("Are you Ok to close this chat by the infomation provided?")
                        .Message("Thanks for contacting Foresters Financials!")
                 .OnCompletion(processRequest)
                .Build();
        }
    }
}







//    public enum RequestOptions { Unknown, CheckStatus, CreateCase };

//    [Serializable]
//    public class Order
//    {
//        public RequestOptions RequestType;
//        [Prompt("What is your first name?")]
//        public string RecipientFirstName;
//        public string RecipientLastName;
//        public string RecipientPhoneNumber;
//        public string UseSavedSenderInfo;
//        public bool AskToUseSavedSenderInfo;
//        public string Note;
//        [Prompt("Please enter your id")]
//        public string Id;


//        public static IForm<Order> BuildOrderForm()
//    {
//        return new FormBuilder<Order>()
//            .Field(nameof(RecipientFirstName))
//            .Field(nameof(RecipientLastName))
//            .Field(nameof(RecipientPhoneNumber))
//            .Field(nameof(Note))
//            .Field(new FieldReflector<Order>(nameof(UseSavedSenderInfo))
//                .SetActive(state => state.AskToUseSavedSenderInfo)
//                .SetNext((value, state) =>
//                {
//                    var selection = (UseSaveInfoResponse)value;

//                    if (selection == UseSaveInfoResponse.Edit)
//                    {
//                        state.SenderEmail = null;
//                        state.SenderPhoneNumber = null;
//                        return new NextStep(new[] { nameof(SenderEmail) });
//                    }
//                    else
//                    {
//                        return new NextStep();
//                    }
//                }))
//            .Field(new FieldReflector<Order>(nameof(SenderEmail))
//                .SetActive(state => !state.UseSavedSenderInfo.HasValue || state.UseSavedSenderInfo.Value == UseSaveInfoResponse.Edit)
//                .SetNext(
//                    (value, state) => (state.UseSavedSenderInfo == UseSaveInfoResponse.Edit)
//                    ? new NextStep(new[] { nameof(SenderPhoneNumber) })
//                    : new NextStep()))
//            .Field(nameof(SenderPhoneNumber), state => !state.UseSavedSenderInfo.HasValue || state.UseSavedSenderInfo.Value == UseSaveInfoResponse.Edit)
//            .Field(nameof(SaveSenderInfo), state => !state.UseSavedSenderInfo.HasValue || state.UseSavedSenderInfo.Value == UseSaveInfoResponse.Edit)
//            .Build();
//    }
//}




