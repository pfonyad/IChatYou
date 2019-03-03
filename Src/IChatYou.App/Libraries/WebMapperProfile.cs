namespace IChatYou.App.Libraries
{

    using AutoMapper;
    using IChatYou.App.ViewModels;
    using IChatYou.DAL.Entities;

    public class WebMapperProfile : Profile
    {
        public WebMapperProfile()
        {
            var dateTimePattern = "yyyy.MM.dd HH:mm:ss";

            CreateMap<MessageView, MessageViewModel>()
                .ForMember(m => m.Sender, desc => desc.MapFrom(mf => mf.Sender.UserName))
                .ForMember(m => m.Sent, desc => desc.MapFrom(mf => mf.Message.Sent.ToString(dateTimePattern)))
                .ForMember(m => m.MessageId, desc => desc.MapFrom(mf => mf.Message.Id))
                .ForMember(m => m.Text, desc => desc.MapFrom(mf => mf.Message.Text));
        }
    }
}