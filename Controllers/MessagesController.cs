using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace SujithBot
{

    public enum HowToOptions
    {
        HowToApplyClaim,WhatIsMyCertificateStatus,NeedForms
    };
    public enum Firs { SixInch, FootLong };
    public enum BreadOptions { NineGrainWheat, NineGrainHoneyOat, Italian, ItalianHerbsAndCheese, Flatbread };
    public enum CheeseOptions { American, MontereyCheddar, Pepperjack };
    public enum ToppingOptions
    {
        ClaimForm, PolicyRequest, Cucumbers, GreenBellPeppers, Jalapenos,
        Lettuce, Olives, Pickles, RedOnion, Spinach, Tomatoes
    };
    public enum SauceOptions
    {
        ChipotleSouthwest, HoneyMustard, LightMayonnaise, RegularMayonnaise,
        Mustard, Oil, Pepper, Ranch, SweetOnion, Vinegar
    };
    [Serializable]
    public class SandwichOrder
    {
        [Prompt("For Authentication, What is your Firstname")]
        public HowToOptions? Need ;
        public BreadOptions? Bread;
        public CheeseOptions? Cheese;
        public List<ToppingOptions> Toppings;
        public List<SauceOptions> Sauce;
        public static IForm<SandwichOrder> BuildForm()
        {
            return new FormBuilder<SandwichOrder>()
                    .Message("Welcome to the Foresters! How could I help You?")
                    .Build();
        }
    };


    //[Serializable]
    //public class EchoDialog : IDialog<object>
    //{
    //    public async Task StartAsync(IDialogContext context)
    //    {
    //        context.Wait(MessageReceivedAsync);
    //    }
    //    public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    //    {
    //        var message = await argument;
    //        await context.PostAsync("You said: " + message.Text);
    //        context.Wait(MessageReceivedAsync);
    //    }
    //}


    [BotAuthentication]
    public class MessagesController : ApiController
    {

        internal static IDialog<ForestersContactBot> BuildInsuranceDialog()
        {
           // return Chain.From(() => FormDialog.FromForm(ForestersContactBot.BuildForm));
            return Chain.From(() => FormDialog.FromForm(ForestersContactBot.BuildForm));
        }





        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        // public async Task<HttpResponseMessage> Post([FromBody]Activity message)
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
           


            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;

                //if (activity.Text.ToLower() == "007")
                //{
                    await Conversation.SendAsync(activity, BuildInsuranceDialog);
                    //return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
                //}




                if (activity.Text.ToLower() == "007")
                {

                    Activity replyToConversation = activity.CreateReply("What you looking for?");
                    replyToConversation.Recipient = activity.From;
                    replyToConversation.Type = "message";
                    replyToConversation.Attachments = new List<Attachment>();
                    List<CardImage> cardImages = new List<CardImage>();
                    cardImages.Add(new CardImage(url: "http://www.lapizzatreno.com/upload/pizza_thumb/pizza_image2376.png"));
                    cardImages.Add(new CardImage(url: "https://mob.kotipizza.fi/kuvat/tuotteet/pizza-berlusconi-200x200.png"));
                    List<CardAction> cardButtons = new List<CardAction>();
                    CardAction plButton = new CardAction()
                    {
                        Value = "http://foresters.com",
                        Type = "openUrl",
                        Title = "Foresters Site"
                    };
                    cardButtons.Add(plButton);
                    HeroCard plCard = new HeroCard()
                    {
                        Title = "I'm your Helper",
                        Subtitle = "Go to foresters page",
                        Images = cardImages,
                        Buttons = cardButtons
                    };
                    Attachment plAttachment = plCard.ToAttachment();
                    replyToConversation.Attachments.Add(plAttachment);
                    var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
                }
                if (activity.Text.ToLower() == "009")
                {
                    Activity replyToConversation = activity.CreateReply("Should go to conversation, with a thumbnail card");
                    replyToConversation.Recipient = activity.From;
                    replyToConversation.Type = "message";
                    replyToConversation.Attachments = new List<Attachment>();
                    List<CardImage> cardImages = new List<CardImage>();
                    cardImages.Add(new CardImage(url: "https://mob.kotipizza.fi/kuvat/tuotteet/pizza-berlusconi-200x200.png"));
                    List<CardAction> cardButtons = new List<CardAction>();
                    CardAction plButton = new CardAction()
                    {
                        Value = "https://en.wikipedia.org/wiki/Pig_Latin",
                        Type = "openUrl",
                        Title = "WikiPedia Page"
                    };
                    cardButtons.Add(plButton);
                    ThumbnailCard plCard = new ThumbnailCard()
                    {
                        Title = "I'm a thumbnail card",
                        Subtitle = "Pig Latin Wikipedia Page",
                        Images = cardImages,
                        Buttons = cardButtons
                    };
                    Attachment plAttachment = plCard.ToAttachment();
                    replyToConversation.Attachments.Add(plAttachment);
                    var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
                }


                if (activity.Text.ToLower() == "3")
                {
                    Activity replyToConversation = activity.CreateReply("Receipt card");
                    replyToConversation.Recipient = activity.From;
                    replyToConversation.Type = "message";
                    replyToConversation.Attachments = new List<Attachment>();
                    List<CardImage> cardImages = new List<CardImage>();
                    cardImages.Add(new CardImage(url: "https://mob.kotipizza.fi/kuvat/tuotteet/pizza-berlusconi-200x200.png"));
                    List<CardAction> cardButtons = new List<CardAction>();
                    CardAction plButton = new CardAction()
                    {
                        Value = "https://en.wikipedia.org/wiki/Pig_Latin",
                        Type = "openUrl",
                        Title = "WikiPedia Page"
                    };
                    cardButtons.Add(plButton);
                    ReceiptItem lineItem1 = new ReceiptItem()
                    {
                        Title = "Pork Shoulder",
                        Subtitle = "8 lbs",
                        Text = null,
                        Image = new CardImage(url: "https://mob.kotipizza.fi/kuvat/tuotteet/pizza-berlusconi-200x200.png"),
                        Price = "16.25",
                        Quantity = "1",
                        Tap = null
                    };
                    ReceiptItem lineItem2 = new ReceiptItem()
                    {
                        Title = "Bacon",
                        Subtitle = "5 lbs",
                        Text = null,
                        Image = new CardImage(url: "https://mob.kotipizza.fi/kuvat/tuotteet/pizza-berlusconi-200x200.png"),
                        Price = "34.50",
                        Quantity = "2",
                        Tap = null
                    };
                    List<ReceiptItem> receiptList = new List<ReceiptItem>();
                    receiptList.Add(lineItem1);
                    receiptList.Add(lineItem2);
                    ReceiptCard plCard = new ReceiptCard()
                    {
                        Title = "I'm a receipt card, isn't this bacon expensive?",
                        Buttons = cardButtons,
                        Items = receiptList,
                        Total = "275.25",
                        Tax = "27.52"
                    };
                    Attachment plAttachment = plCard.ToAttachment();
                    replyToConversation.Attachments.Add(plAttachment);
                    var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
                }

                //if (activity.Text.ToLower() == "4")
                //{
                //    Activity replyToConversation = activity.CreateReply("Should go to conversation, sign-in card");
                //    replyToConversation.Recipient = activity.From;
                //    replyToConversation.Type = "message";
                //    replyToConversation.Attachments = new List<Attachment>();
                //    List<CardAction> cardButtons = new List<CardAction>();
                //    CardAction plButton = new CardAction()
                //    {
                //        Value = "https://<OAuthSignInURL>",
                //        Type = "signin",
                //        Title = "Connect"
                //    };
                //    cardButtons.Add(plButton);
                //    SigninCard plCard = new SigninCard()
                //{
                //    Text = "You need to authorize me"
                //};//title : 'You need to authorize me', button: plButton");
                //    Attachment plAttachment = plCard.ToAttachment();
                //    replyToConversation.Attachments.Add(plAttachment);
                //    var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
                //}

                if (activity.Text.ToLower() == "008")
                {
                    Activity replyToConversation = activity.CreateReply("Should go to conversation, with a carousel");
                    replyToConversation.Recipient = activity.From;
                    replyToConversation.Type = "message";
                    replyToConversation.AttachmentLayout = "carousel"; //AttachmentLayouts.Carousel;
                    replyToConversation.Attachments = new List<Attachment>();
                    Dictionary<string, string> cardContentList = new Dictionary<string, string>();
                    cardContentList.Add("PigLatin", "https://mob.kotipizza.fi/kuvat/tuotteet/pizza-berlusconi-200x200.png");
                    cardContentList.Add("Pork Shoulder", "https://mob.kotipizza.fi/kuvat/tuotteet/pizza-berlusconi-200x200.png");
                    cardContentList.Add("Bacon", "https://mob.kotipizza.fi/kuvat/tuotteet/pizza-berlusconi-200x200.png");
                    foreach (KeyValuePair<string, string> cardContent in cardContentList)
                    {
                        List<CardImage> cardImages = new List<CardImage>();
                        cardImages.Add(new CardImage(url: cardContent.Value));
                        List<CardAction> cardButtons = new List<CardAction>();
                        CardAction plButton = new CardAction()
                        {
                            Value = "https://en.wikipedia.org/wiki/{cardContent.Key}",
                            Type = "openUrl",
                            Title = "WikiPedia Page"
                        };
                        cardButtons.Add(plButton);
                        HeroCard plCard = new HeroCard()
                        {
                            Title =  String.Format("I'm a hero card about {0}",cardContent.Key),
                            Subtitle = String.Format("{0} Wikipedia Page",cardContent.Key),
                            Images = cardImages,
                            Buttons = cardButtons
                        };
                        Attachment plAttachment = plCard.ToAttachment();
                        replyToConversation.Attachments.Add(plAttachment);
                    }
                    replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                    var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);

                }
            }
            else
            {
                HandleSystemMessage(activity);
            }

            // check if activity is of type message
            //if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            //{
            //    await Conversation.SendAsync(activity, () => new EchoDialog());
            //}
            //else
            //{
            //    HandleSystemMessage(activity);
            //}
          //  return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);


            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }


        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }


        //private string GetMe(Message)
        //{
        //    string FoodItem = (message.Text).ToLower();
        //    if (FoodItem == "pizza")
        //    {
        //        Attachment attachment1 = new Attachment();
        //        attachment1.ContentType = "image/png";
        //        attachment1.ContentUrl = "http://www.lapizzatreno.com/upload/pizza_thumb/pizza_image2376.png";

        //        Attachment attachment2 = new Attachment();
        //        attachment2.ContentType = "image/png";
        //        attachment2.ContentUrl = "https://mob.kotipizza.fi/kuvat/tuotteet/pizza-berlusconi-200x200.png";

        //        message.Attachments.Add(attachment1);
        //        message.Attachments.Add(attachment2);

        //        message.Text = "Which pizza you want ";
        //        return message;
        //    }
        //    else if (FoodItem == "burger")
        //    {
        //        Attachment attachment1 = new Attachment();
        //        attachment1.ContentType = "image/jpg";
        //        attachment1.ContentUrl = "http://insidescoopsf.sfgate.com/files/2015/07/burger-200x200.jpg";

        //        Attachment attachment2 = new Attachment();
        //        attachment2.ContentType = "image/jpg";
        //        attachment2.ContentUrl = "http://www.mymarios.com/site/wp-content/uploads/1972/06/burgers-veggie-burger-200x200.jpg";

        //        message.Attachments.Add(attachment1);
        //        message.Attachments.Add(attachment2);

        //        message.Text = "Which Burger you want ";
        //        return message;
        //    }
        //    else
        //    {
        //        message.CreateReplyMessage("Hello, You can order for Pizza and Burger. Just Type \"Pizza\" for Pizza and \"Burger\" for Burger. ");
        //    }

        //    return message; 
        //}
    }
}