using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.WebApp.Controllers.Abstract;
using EnsafMarket.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnsafMarket.WebApp.Controllers
{
    [RoutePrefix("Contact")]
    public class ContactController : BaseController
    {
        [Route]
        public async Task<ActionResult> Index()
        {
            return HttpNotFound();
        }

        [Route("{id:int}", Name = "Contact Details")]
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            await GetUserAsync();
            var response = await emClient.GetContactAsync((int)id);
            if (response.Contact == null)
            {
                return HttpNotFound();
            }
            var contact = response.Contact;

            var advertisementResponse = await emClient.GetAdvertisementAsync(contact.AdvertisementId);
            contact.Advertisement = advertisementResponse.Advertisement;

            var ownerResponse = await emClient.GetUserAsync(contact.Advertisement.OwnerId);
            contact.Advertisement.Owner = ownerResponse.User;

            if (contact.UserId == user.Id)
            {
                contact.User = user;
            }
            else
            {
                var userResponse = await emClient.GetUserAsync((int)contact.UserId);
                contact.User = userResponse.User;
            }

            if (user.Id != contact.UserId && user.Id != contact.Advertisement.OwnerId)
            {
                return HttpNotFound();
            }

            var feedbacksResponse = await emClient.GetContactFeedbacksAsync(contact.Id);
            var ownerFeedback = feedbacksResponse.OwnerFeedback;
            var userFeedback = feedbacksResponse.UserFeedback;

            var contactMessagesResponse = await emClient.GetContactMessagesAsync(contact.Id);
            var contactMessages = contactMessagesResponse.ContactMessages;

            foreach(var message in contactMessages)
            {
                message.User = message.UserId == user.Id ? user : contact.Advertisement.Owner;
            }

            var model = new ContactDetailsModelView
            {
                User = user,
                Contact = contact,
                Messages = contactMessages,
                OwnerFeedback = ownerFeedback,
                UserFeedback = userFeedback
            };

            if(model.MyFeedback != null)
            {
                model.Rating = model.MyFeedback.Rating;
                model.Feedback = model.MyFeedback.Feedback;
            }
            return View(model);
        }

        [Route("{id:int}")]
        [HttpPost]
        public async Task<ActionResult> Details(int id, ContactDetailsModelView model)
        {
            await GetUserAsync();
            var response = await emClient.GetContactAsync((int)id);
            if (response.Contact == null)
            {
                return HttpNotFound();
            }
            var contact = response.Contact;

            var advertisementResponse = await emClient.GetAdvertisementAsync(contact.AdvertisementId);
            contact.Advertisement = advertisementResponse.Advertisement;

            var ownerResponse = await emClient.GetUserAsync(contact.Advertisement.OwnerId);
            contact.Advertisement.Owner = ownerResponse.User;

            if(contact.UserId == user.Id)
            {
                contact.User = user;
            }
            else
            {
                var userResponse = await emClient.GetUserAsync((int)contact.UserId);
                contact.User = userResponse.User;
            }

            if (user.Id != contact.UserId && user.Id != contact.Advertisement.OwnerId)
            {
                return HttpNotFound();
            }
            
            if(model.Action == "Message" && !string.IsNullOrWhiteSpace(model.ContactMessage)) {
                var postContactMessageResponse = await emClient.PostContactMessageAsync((int)id, new PostContactMessageRequest
                {
                    Content = model.ContactMessage
                });
                
                if(postContactMessageResponse.Result)
                    model.ContactMessage = null;
                else
                {
                    model.MessageErrorMessage = postContactMessageResponse.Message;
                }
            }
            
            if(model.Action == "Feedback")
            {
                if(model.Rating >= 1 && model.Rating <= 5)
                {
                    var postFeedbackResponse = await emClient.PostContactFeedbackAsync((int)id, new PostContactFeedbackRequest
                    {
                        Feedback = model.Feedback,
                        Rating = model.Rating
                    });

                    if(!postFeedbackResponse.Result)
                    {
                        model.FeedbackErrorMessage = postFeedbackResponse.Message;
                    }
                }
                else
                {
                    model.FeedbackErrorMessage = "La note doit être entre 1 et 5";
                }
            }

            if (model.Action == "UpdateFeedback")
            {
                if (model.Rating >= 1 && model.Rating <= 5)
                {
                    var putFeedbackResponse = await emClient.PutContactFeedbackAsync((int)id, new PutContactFeedbackRequest {
                        Feedback = model.Feedback,
                        Rating = model.Rating
                    });

                    if (!putFeedbackResponse.Result)
                    {
                        model.FeedbackErrorMessage = putFeedbackResponse.Message;
                    }
                }
                else
                {
                    model.FeedbackErrorMessage = "La note doit être entre 1 et 5";
                }
            }

            var feedbacksResponse = await emClient.GetContactFeedbacksAsync(contact.Id);
            var ownerFeedback = feedbacksResponse.OwnerFeedback;
            var userFeedback = feedbacksResponse.UserFeedback;

            var contactMessagesResponse = await emClient.GetContactMessagesAsync(contact.Id);
            var contactMessages = contactMessagesResponse.ContactMessages;

            foreach (var message in contactMessages)
            {
                message.User = message.UserId == user.Id ? user : contact.Advertisement.Owner;
            }
            model.User = user;
            model.Contact = contact;
            model.Messages = contactMessages;
            model.OwnerFeedback = ownerFeedback;
            model.UserFeedback = userFeedback;
            return View(model);
        }
    }
}