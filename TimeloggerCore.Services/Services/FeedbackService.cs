using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Services
{
    public class FeedbackService : BaseService<FeedbackModel, Feedback, int>, IFeedbackService
    {
        private readonly IFeedbackRepository feedbackRepository;

        public FeedbackService(IMapper mapper, IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork) : base(mapper, feedbackRepository, unitOfWork)
        {
            this.feedbackRepository = feedbackRepository;
        }
    }
}
