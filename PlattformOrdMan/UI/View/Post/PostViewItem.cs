﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PlattformOrdMan.Data;
using PlattformOrdMan.Data.Conf;

namespace PlattformOrdMan.UI.View.Post
{
    public class PostViewItem : ListViewItem
    {
        private Data.PostData.Post _post;

        public PostViewItem(Data.PostData.Post post)
            : base("")
        {
            _post = post;
            InitContents();
        }

        private void InitContents()
        {
            var columns = PostListView.GetColumns();
            Text = columns.First().GetString(_post);
            columns.Skip(1).ToList().ForEach((c) => { SubItems.Add(c.GetString(_post)); });
            SetStatusColor();
        }

        public void ReloadPost(Data.PostData.Post post)
        {
            _post = post;
            UpdateViewItem();
        }

        public void ReloadSupplier(Supplier supplier)
        {
            _post.ReloadSupplier(supplier);
            UpdateViewItem();
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            _post.ReloadMerchandise(merchandise);
            UpdateViewItem();
        }

        public Data.PostData.Post GetPost()
        {
            return _post;
        }

        private void SetStatusColor()
        {
            if (_post.AttentionFlag)
            {
                ForeColor = Color.Black;
                BackColor = Color.Red;
                return;
            }
            if (!_post.IsMerchandiseEnabled())
            {
                ForeColor = Color.Red;
                ToolTipText = "This product is not up to date";
            }
            switch (_post.GetPostStatus())
            {
                case Data.PostData.Post.PostStatus.Booked:
                    BackColor = Color.LightCoral;
                    ForeColor = Color.Black;
                    break;
                case Data.PostData.Post.PostStatus.Ordered:
                    BackColor = Color.Yellow;
                    ForeColor = Color.Black;
                    break;
                case Data.PostData.Post.PostStatus.ConfirmedOrder:
                    BackColor = Color.LightBlue;
                    ForeColor = Color.Black;
                    break;
                case Data.PostData.Post.PostStatus.Confirmed:
                    BackColor = Color.Lime;
                    ForeColor = Color.Black;
                    break;
                case Data.PostData.Post.PostStatus.Completed:
                    if (_post.Periodization.HasValue)
                    {
                        BackColor = Color.DarkMagenta;
                        ForeColor = Color.White;
                    }
                    else
                    {
                        BackColor = Color.White;
                        ForeColor = Color.Black;
                    }                    break;
            }
        }

        public void UpdateViewItem()
        {
            var columns = PostListView.GetColumns();
            var i = 0;
            columns.ToList().ForEach(c =>
            {
                SubItems[i].Text = c.GetString(_post);
                i++;
            });
            SetStatusColor();
        }
    }
}