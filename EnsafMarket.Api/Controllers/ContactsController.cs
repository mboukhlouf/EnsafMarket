using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsafMarket.Api.Authorization;
using EnsafMarket.Api.Models;
using EnsafMarket.Core.Models;
using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.Core.Models.Api.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnsafMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly EnsafMarketDbContext context;

        public ContactsController(EnsafMarketDbContext context)
        {
            this.context = context;
        }

        // GET: api/Contact/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetContactResponse>> GetContactAsync(int id)
        {
            var contact = await context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound(new GetContactResponse
                {
                    Result = false,
                    Message = "Not found"
                });
            }

            context.Entry(contact).Reference(nameof(contact.Advertisement)).Load();

            UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
            if (userClaims == null || !(
                userClaims.Id == contact.Advertisement.OwnerId ||
                userClaims.Id == contact.UserId))
            {
                return Unauthorized(new GetContactResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }
            contact.Advertisement = null;
            return new GetContactResponse
            {
                Result = true,
                Message = "",
                Contact = contact
            };
        }

        // POST: api/Contact
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostContactResponse>> PostContactAsync(PostContactRequest request)
        {
            PostContactResponse response = new PostContactResponse();
            if (!ModelState.IsValid)
            {
                response.Result = false;
                response.Message = "Model not valid";
                return BadRequest(response);
            }

            UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
            if (userClaims == null)
            {
                response.Result = false;
                response.Message = "Unauthorized";
                return Unauthorized(response);
            }

            var advertisement = await context.Advertisement.FindAsync(request.AdvertisementId);
            if (advertisement == null)
            {
                response.Result = false;
                response.Message = "Advertisement not found.";
                return NotFound(response);
            }

            if(advertisement.OwnerId == userClaims.Id)
            {
                response.Result = false;
                response.Message = "You can't create a contact with yourself";
                return Unauthorized(response);
            }

            var contact = new Contact
            {
                AdvertisementId = (int)advertisement.Id,
                UserId = userClaims.Id
            };

            context.Contact.Add(contact);
            await context.SaveChangesAsync();

            response.Result = true;
            response.Message = "Contact created.";
            response.Contact = contact;
            response.Contact.Advertisement = null;
            return CreatedAtAction("GetContact", new { id = contact.Id }, response);
        }

        [HttpGet("{id}/Messages")]
        [Authorize]
        public async Task<ActionResult<GetContactMessagesResponse>> GetContactMessagesAsync(int id)
        {
            UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
            if (userClaims == null)
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            var contact = await context.Contact.FindAsync(id);
            contact.Advertisement = await context.Advertisement.FindAsync(contact.AdvertisementId);

            if(userClaims.Id != contact.UserId &&
                userClaims.Id != contact.Advertisement.OwnerId)
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            var contactMessages = context.ContactMessage.Where(message => message.ContactId == id)
                .OrderByDescending(message => message.DateTime)
                .ToList();

            var response = new GetContactMessagesResponse
            {
                Result = true,
                ContactMessages = contactMessages
            };
            return response;
        }

        [HttpPost("{id}/Messages")]
        [Authorize]
        public async Task<ActionResult<PostContactMessageResponse>> PostContactMessageAsync(int id, PostContactMessageRequest request)
        {
            UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
            if (userClaims == null)
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            var contact = await context.Contact.FindAsync(id);
            contact.Advertisement = await context.Advertisement.FindAsync(contact.AdvertisementId);

            if (userClaims.Id != contact.UserId &&
                userClaims.Id != contact.Advertisement.OwnerId)
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            var contactMessage = new ContactMessage
            {
                ContactId = id,
                UserId = userClaims.Id,
                Content = request.Content
            };

            context.ContactMessage.Add(contactMessage);
            await context.SaveChangesAsync();

            return Created("", new PostContactMessageResponse
            {
                Result = true,
                ContactMessage = contactMessage
            });
        }
    
        [HttpGet("{id}/Feedbacks")]
        [Authorize]
        public async Task<ActionResult<GetContactFeedbacksResponse>> GetContactFeedbacksAsync(int id)
        {
            UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
            if (userClaims == null)
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            var contact = await context.Contact.FindAsync(id);
            context.Entry(contact).Reference(nameof(contact.Advertisement)).Load();
            context.Entry(contact).Collection(nameof(contact.Feedbacks)).Load();

            if (userClaims.Id != contact.UserId &&
                userClaims.Id != contact.Advertisement.OwnerId)
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }


            var response = new GetContactFeedbacksResponse
            {
                Result = true,
                UserFeedback = contact.UserFeedback,
                OwnerFeedback = contact.OwnerFeedback
            };
            return response;
        }

        [HttpPost("{id}/Feedbacks")]
        [Authorize]
        public async Task<ActionResult<PostContactFeedbackResponse>> PostContactFeedbacksAsync(int id, PostContactFeedbackRequest request)
        {
            UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
            if (userClaims == null)
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            var contact = await context.Contact.FindAsync(id);
            context.Entry(contact).Reference(nameof(contact.Advertisement)).Load();
            context.Entry(contact).Collection(nameof(contact.Feedbacks)).Load();

            if (userClaims.Id != contact.UserId &&
                userClaims.Id != contact.Advertisement.OwnerId)
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            if(contact.Feedbacks != null && contact.Feedbacks.Any(feedback => feedback.FromUserId == userClaims.Id))
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "You already have posted a feedback in this contact."
                });
            }

            int fromId = userClaims.Id;
            int toId;
            if (fromId == contact.UserId)
            {
                toId = contact.Advertisement.OwnerId;
            } 
            else
            {
                toId = (int)contact.UserId;
            }

            var contactFeedback = new ContactFeedback
            {
                ContactId = id,
                Rating = request.Rating,
                Feedback = request.Feedback,
                FromUserId = fromId,
                ToUserId = toId
            };

            context.ContactFeedback.Add(contactFeedback);
            await context.SaveChangesAsync();

            return Created("", new PostContactFeedbackResponse
            {
                Result = true,
                ContactFeedback = contactFeedback
            });
        }

        [HttpPut("{id}/Feedbacks")]
        [Authorize]
        public async Task<ActionResult<PutContactFeedbackResponse>> PutContactFeedbacksAsync(int id, PutContactFeedbackRequest request)
        {
            UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
            if (userClaims == null)
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            var contact = await context.Contact.FindAsync(id);
            context.Entry(contact).Reference(nameof(contact.Advertisement)).Load();
            context.Entry(contact).Collection(nameof(contact.Feedbacks)).Load();

            if (userClaims.Id != contact.UserId &&
                userClaims.Id != contact.Advertisement.OwnerId)
            {
                return Unauthorized(new GetContactMessagesResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            var feedback = contact.Feedbacks?.FirstOrDefault(fdback => fdback.FromUserId == userClaims.Id);
            if (feedback == null)
            {
                return BadRequest(new PutContactFeedbackResponse
                {
                    Result = false,
                    Message = "There is no feedback to update"
                });
            }

            feedback.Rating = request.Rating;
            feedback.Feedback = request.Feedback;

            context.Entry(feedback).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return new PutContactFeedbackResponse
            {
                Result = true,
                ContactFeedback = feedback
            };
        }
    }
}