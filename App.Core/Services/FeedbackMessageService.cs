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
using static App.Infrastructure.Constants.DataConstants;

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
                Severity = m.SeverityType.Name

            }).ToListAsync();
            return messages;

        }

        public async Task<IEnumerable<FeedbackMessageViewModel>> AdminGetAllMessagesAsync()
        {
            var messages = await _context.FeedbackMessages.AsNoTracking().Select(m => new FeedbackMessageViewModel()
            {
                SenderUsername = m.SenderUser.UserName,
                Title = m.Title,
                Content = m.Content,
                Date = m.Date,
                Comment = m.Comment,
                Severity = m.SeverityType.Name,

            }).ToListAsync();
            return messages;

        }

        public async Task CreateMessageAsync(FeedbackMessageFormModel model, string userId)
        {
            var message = new FeedbackMessage()
            {
                SenderId = userId,
                Title = model.Title,
                Content = model.Content,
                Date = DateTime.Now,
            };
            await _context.FeedbackMessages.AddAsync(message);
        } 

        public async Task UpdateSeverityStatusOnMessageAsync(int messageId, int severityId)
        {
            var message=await _context.FeedbackMessages.FindAsync(messageId);
            if(message != null&&await _context.SeverityTypes.AnyAsync(s=>s.Id==severityId))
            {
               message.SeverityTypeId=severityId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddCommentToMessageAsync(int id,string comment)
        {
            var message = await _context.FeedbackMessages.FindAsync(id);
            message.Comment = comment;
            await _context.SaveChangesAsync();
        }

        public async Task<FeedbackMessageFormModel> FindMessageByIdAsync(int id)
        {
            var message =await _context.FeedbackMessages.FindAsync(id);
            return new FeedbackMessageFormModel()
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
    }
}
