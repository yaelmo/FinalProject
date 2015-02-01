﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using WebApplication1.BL;
using WebApplication1.classes;
using WebApplication1.Classes;

namespace WebApplication1.Pages
{
    public partial class c : System.Web.UI.Page
    {
        private QuestionnaireBL questionnaireBL;
        private CourseBL courseBL;
        static QuestionBL questionBL;
        public List<Questionnaire> listQuestionnaire;
        public static List<Question> listQuestion;
        private static int idCourse = 0;
        private String CourseName;
        private static GlobalFunction global;

        protected void Page_Load(object sender, EventArgs e)
        {

            questionnaireBL= new QuestionnaireBL();
            courseBL = new CourseBL();
            questionBL = new QuestionBL();
            listQuestionnaire = new List<Questionnaire>();
            listQuestion = new List<Question>();
            global = new GlobalFunction();
            courseBL = new CourseBL();
            

            if (Request.QueryString["IdCourse"] != null)
            {

                //Response.Write("<script language=javascript>alert('השדה שאתה רוצה למחוק בשימוש');</script>");
                idCourse = Convert.ToInt32(Request.QueryString["IdCourse"]);
                //CourseNameLabe.InnerText = courseBL.getNameById(idCourse).ToString();
                CourseName = courseBL.getNameById(idCourse).ToString();

                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Session["coursId"] + "');", true);
                listQuestionnaire = questionnaireBL.getAllQuestionnaireByIdCours(idCourse);

               
            }
            else
            {
                //CourseNameLabe.InnerText = "חיפוש";

                CourseName = "חיפוש";
                listQuestionnaire = questionnaireBL.getAllQuestionnaireByPermit();
            }
            
        }

        public String getCourseName()
        {
            return CourseName;
        }



        public void onClick_Questionnaire(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
            stockQuestionnaire.Style.Add("display", "none");
            StockQuestion.Style.Add("display", "inline");
            Response.Write("<script language=javascript>alert('השדה שאתה רוצה למחוק בשימוש');</script>");

            listQuestion = questionBL.getAllQuestionByQuestionnaire(Convert.ToInt32(QuestionnaireId.Value));
        }

       [WebMethod]
        public static void removeCourse()
        {
            global.removeLecurerCourseFromDB(idCourse); // remove Lecurer course
         

        }

       //free session and redirect to login page.
       protected void logout_click(object sender, EventArgs e)
       {
           Session.Abandon();
           Response.Redirect("logIn.aspx");
       }
    }
}