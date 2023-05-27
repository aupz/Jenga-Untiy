using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeInformation : MonoBehaviour
{
    StudentGrade studentGrade;


    public StudentGrade getStudentGrade(){
        return studentGrade;
    }

    public void setStudentGrade(StudentGrade studentGrade){
        this.studentGrade = studentGrade;
    }

}
