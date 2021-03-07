using System;
using System.Collections.Generic;

namespace Paination.Models
{
    public class Pagin
    {
        public int PageSize { get;private set; }=10; //amount of result per page
        public int TotalItems { get;private set; } // we count records from the database and fill it here
        public int CurrentPage { get;private set; }
        public int TotalPages {get;private set;}
        public int StartPage { get;private set; }
        public int EndPage { get;private set; }
        public Pagin(){}
        public Pagin(int totalItems,int currentPage)//from the action we will send the record count and the page user is at
        {
            int totalPages=(int)Math.Ceiling(decimal.Divide(totalItems,PageSize));//calculate the amount of pages we get
            
            //now for start and end page
            //imagine we have more than 20 pages
            //and the user is at page 10
            //we want the pagination to be like this   1 2 ... 7 8 9 10 11 12 13 ... 19 20    
            //the user is at 10 and it shows 7 as minimum because 10 -3 =7
            //and end page is 13 because 10 + 3=13 
            int startPage=currentPage-3; 
            int endPage=currentPage+3;


            //if start page is bellow zero after calculation
            // so in this example this block of code will run if user is at page 1 2 3 
            if(startPage<=0)
            {
                //if user start at 1 start page will be -4
                // end page will be 4 then 4-(-4)=8
                //so we see 8 pages excluding the selected page 
                endPage=endPage-(startPage);
                startPage=1;//reset startpage to 1 because it cannot be negative number
            }
            if(endPage>totalPages)//if the end page bigger than total page after calculation
            {
                //if we have total of 21 pages 
                //and user is at 19 
                //19+3 will be 22
                endPage=totalPages;//reset endpage to total page which is 21
                if(endPage>10)//if endpage is bigger than 10 which it is
                {
                    //subtract 5 from it and asign it to start page
                    //21-5=16 so we will see start page at 16
                    startPage=endPage-5; //this will exclude (... 20 21) but the code block above wont 
                    //make sure startpage from this code block match visualy from code block above, above block is 8 this block is 5 + three more pages
                }
            }
            //asign result to properties
            TotalItems=totalItems;
            CurrentPage=currentPage;
            TotalPages=totalPages;
            StartPage=startPage;
            EndPage=endPage;
        }
    }
}