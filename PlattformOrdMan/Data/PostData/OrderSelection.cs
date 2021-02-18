using System;
using System.Collections.Generic;
using System.Linq;
using Molmed.PlattformOrdMan.Data;

namespace PlattformOrdMan.Data.PostData
{
    public class OrderSelection
    {
        private readonly List<Post> _posts;

        public OrderSelection(List<Post> posts)
        {
            _posts = posts;
        }

        public List<OrderPostDto> GenerateFromCommonInput(Enquiry account, Enquiry periodization)
        {
            return GenerateValues(post => account, post => periodization);
        }

        public List<OrderPostDto> GenerateFromCurrent()
        {
            return GenerateValues(post => post.Account, post => post.Periodization);
        }

        private List<OrderPostDto> GenerateValues(
            Func<Post, Enquiry> accountFunc, Func<Post, Enquiry> periodizationFunc)
        {
            List<OrderPostDto> generated = new List<OrderPostDto>();
            _posts.ForEach(p =>
            {
                var bag = new OrderPostDto
                {
                    PostId = p.GetId(),
                    OrderedUserId = UserManager.GetCurrentUser().GetId(),
                    Account = accountFunc(p),
                    Periodization = periodizationFunc(p)
                };
                generated.Add(bag);
            });
            return generated;
        }

        public void SignAsOrdered(List<OrderPostDto> postBags)
        {
            postBags.ForEach(bag =>
            {
                var post = _posts.FirstOrDefault(p => p.GetId() == bag.PostId);
                if (post != null) post.OrderPost(bag);
            });
        }

        public PostList PostList()
        {
            PostList list = new PostList();
            _posts.ForEach(p => { list.Add(p);});
            return list;
        }

        public bool AllPostsEmpty()
        {
            return _posts.TrueForAll(p => !p.Account.HasAnswered && !p.Periodization.HasAnswered);
        }

        public bool AllHasMandatory()
        {
            return _posts.TrueForAll(p => p.Periodization.HasAnswered && p.Account.HasAnswered);
        }
        

    }
}
