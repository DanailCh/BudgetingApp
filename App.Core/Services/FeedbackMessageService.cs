using App.Core.Contracts;
using App.Core.Models.FeedbackMessage;
using HouseholdBudgetingApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using App.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using static App.Core.Constants.FeedbackMessageConstants;
using App.Core.Enum;
using App.Core.Models.Archive.Bill;

namespace App.Core.Services
{
    public class FeedbackMessageService : IFeedBackMessageService
    {
        private readonly ApplicationDbContext _context;
        public FeedbackMessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedbackMessageViewModel>> GetAllMessagesAsync(string userId)
        {
            var messages = await _context.FeedbackMessages.AsNoTracking().Where(m => m.SenderId == userId).Select(m => new FeedbackMessageViewModel()
            {
                Title = m.Title,
                Content = m.Content,
                Date = m.Date,
                Comment = m.Comment,

            }).ToListAsync();
            return messages;

        }

        public async Task<FeedbackQueryModel> AdminGetAllMessagesAsync(string userId, AllFeedbackQueryModel model)
        {
            var messagesToShow = _context.FeedbackMessages.AsQueryable();

            if (model.SeverityTypeId != 0 && model.StatusId == 1)
            {
                model.StatusId = 0;
            }

            if (model.SeverityTypeId != 0 || model.StatusId != 0)
            {
                if (model.SeverityTypeId != 0 && model.StatusId != 0)
                {
                    messagesToShow = messagesToShow
                   .Where(b => b.SeverityTypeId == model.SeverityTypeId && b.StatusId == model.StatusId);
                }
                if (model.StatusId != 0&& model.SeverityTypeId == 0)
                {
                    messagesToShow = messagesToShow
                  .Where(b =>  b.StatusId == model.StatusId);
                }
                if (model.SeverityTypeId != 0 && model.StatusId == 0)
                {
                    messagesToShow = messagesToShow
                  .Where(b => b.SeverityTypeId == model.SeverityTypeId);
                }
            }
            




            switch (model.Sorting)
            {
                case MessageSorting.DateAscending:
                    messagesToShow = messagesToShow
                            .OrderBy(b => b.Date);
                    break;
                case MessageSorting.DateDescending:
                    messagesToShow = messagesToShow
                            .OrderByDescending(b => b.Date);
                    break;
                case MessageSorting.None:
                    messagesToShow = messagesToShow
                            .OrderByDescending(b => b.Id);
                    break;

            };

            var messages = await messagesToShow
                .Skip((model.CurrentPage - 1) * model.MessagesPerPage)
                .Take(model.MessagesPerPage)
                .Select(b => new FeedbackMessageFormViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                   Content = b.Content,
                   SenderUsername=b.SenderUser.UserName,
                    Date = b.Date,
                    SeverityId = b.SeverityTypeId ?? 0,
                    StatusId=b.StatusId
                })
                .ToListAsync();

            int totalMessages = await messagesToShow.CountAsync();

            return new FeedbackQueryModel()
            {
                Messages=messages,
                MessagesCount=totalMessages,
            };
        }

        public async Task CreateMessageAsync(FeedbackMessageFormModel model, string userId)
        {
            var status = await _context.Statuses.FirstOrDefaultAsync(s=>s.Name=="New");
            var message = new FeedbackMessage()
            {
                SenderId = userId,
                Title = model.Title,
                Content = model.Content,
                Date = DateTime.Now,
                StatusId= status.Id
        };
            await _context.FeedbackMessages.AddAsync(message);
            await _context.SaveChangesAsync();
        } 

        public async Task SetSeverityStatusOnMessageAsync(int messageId, int severityId)
        {
            var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Name == "In Progress");
            var message=await _context.FeedbackMessages.FindAsync(messageId);
            var IsThere = await _context.SeverityTypes.AnyAsync(s => s.Id == severityId);
            if (message != null&& IsThere)
            {
               message.SeverityTypeId=severityId;
                message.StatusId = status.Id;
                message.Comment=StatusChangeComment;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<FeedbackMessageFormViewModel> FindMessageByIdAsync(int id)
        {
            var message =await _context.FeedbackMessages.FindAsync(id);
            return new FeedbackMessageFormViewModel()
            {
                Id = id,
                Title = message.Title,
                Content = message.Content,
                Comment = message.Comment,
                SeverityId = message.SeverityTypeId ?? 0,
            };            
        }

        public async Task<IEnumerable<SeverityTypeViewModel>> GetSeverityTypesAsync()
        {
           var types=await _context.SeverityTypes.AsNoTracking().Select(s=>new SeverityTypeViewModel()
           {
               Id=s.Id,
               Name=s.Name,
           }).ToListAsync();
            return types;           
        }

        public  Task<bool> MessageExistsAsync(int id)
        {
            return  _context.FeedbackMessages.AnyAsync(m=>m.Id==id);
        }

        public Task<bool> SeverityTypeExistsAsync(int id)
        {
            return _context.SeverityTypes.AnyAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<StatusViewModel>> GetStatusesAsync()
        {
            return await _context.Statuses.AsNoTracking().Select(s => new StatusViewModel()
            {
                Id= s.Id,
                Name = s.Name,
            }).ToListAsync();
        }

        public async Task SetDoneStatusOnMessageAsync(int messageId)
        {
            var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Name == "Done");
            var message = await _context.FeedbackMessages.FindAsync(messageId);
            if (message != null)
            {
                message.StatusId = status.Id;
                message.Comment = ResolveComment;
                await _context.SaveChangesAsync();
            }
        }

        public string GetTextColor(string input)
        {
            switch (input)
            {
                case "Low":
                    return "info";
                case "Medium":
                    return "warning";
                case "High":
                    return "danger";
                case "New":
                    return "secondary";
                case "In Progress":
                    return "primary";
                case "Done":
                    return "success";
            }
            return string.Empty;
        }
    }
}
