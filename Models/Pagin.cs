using System;
using System.Collections.Generic;

namespace Paination.Models
{
    public class Pagin
    {
        public int PageSize { get;private set; }
        public int TotalItems { get;private set; }
        public int CurrentPage { get;private set; }
        public int TotalPages {get;private set;}
        public int StartPage { get;private set; }
        public int EndPage { get;private set; }
        public Pagin(){}
        public Pagin(int totalItems,int page,int pageSize=10)
        {
            int totalPages=(int)Math.Ceiling(decimal.Divide(totalItems,pageSize));
            int currentPage=page;
            int startPage=currentPage-5;
            int endPage=currentPage+4;
            if(startPage<=0)
            {
                endPage=endPage-(startPage-1);
                startPage=1;
            }
            if(endPage>totalPages)
            {
                endPage=totalPages;
                if(endPage>10)
                {
                    startPage=endPage-9;
                }
            }
            TotalItems=totalItems;
            CurrentPage=currentPage;
            PageSize=pageSize;
            TotalPages=totalPages;
            StartPage=startPage;
            EndPage=endPage;
        }
    }
}