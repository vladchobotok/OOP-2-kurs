package xmlclasses;

import java.util.ArrayList;
import java.util.List;

public class Scientists {

    protected List<Scientists.Scientist> scientist;

    public List<Scientists.Scientist> getScientist() {
        if (scientist == null) {
            scientist = new ArrayList<Scientists.Scientist>();
        }
        return this.scientist;
    }

    public static class Scientist implements Comparable<Scientist>{

        protected String surname;
        protected String name;
        protected String middleName;
        protected Scientists.Scientist.Faculty faculty;
        protected String cathedra;
        protected String degree;
        protected Scientists.Scientist.Status status;

        public Scientist(String surname, String name, String middleName,
                      Faculty faculty,
                      String cathedra, String degree,
                      Status status){
            this.surname = surname;
            this.name = name;
            this.middleName = middleName;
            this.faculty = faculty;
            this.cathedra = cathedra;
            this.degree = degree;
            this.status = status;
        }

        public Scientist(){
        }

        public String getSurname() {
            return surname;
        }

        public void setSurname(String value) {
            this.surname = value;
        }

        public String getName() {
            return name;
        }

        public void setName(String value) {
            this.name = value;
        }

        public String getMiddleName() {
            return middleName;
        }

        public void setMiddleName(String value) {
            this.middleName = value;
        }

        public Scientists.Scientist.Faculty getFaculty() {
            return faculty;
        }

        public void setFaculty(Scientists.Scientist.Faculty value) {
            this.faculty = value;
        }

        public String getCathedra() {
            return cathedra;
        }

        public void setCathedra(String value) {
            this.cathedra = value;
        }

        public String getDegree() {
            return degree;
        }

        public void setDegree(String value) {
            this.degree = value;
        }

        public Scientists.Scientist.Status getStatus() {
            return status;
        }

        public void setStatus(Scientists.Scientist.Status value) {
            this.status = value;
        }

        @Override
        public int compareTo(Scientist o) {
            int result = surname.compareTo(o.surname);
            if (result != 0) {
                return result;
            }
            result = name.compareTo(o.name);
            if (result != 0) {
                return result;
            }
            result = middleName.compareTo(o.middleName);
            if (result != 0) {
                return result;
            }
            result = cathedra.compareTo(o.cathedra);
            if (result != 0) {
                return result;
            }
            result = degree.compareTo(o.degree);
            if (result != 0) {
                return result;
            }
            result = faculty.department.compareTo(o.faculty.department);
            if (result != 0) {
                return result;
            }
            result = faculty.section.compareTo(o.faculty.section);
            if (result != 0) {
                return result;
            }
            result = status.academicStatus.compareTo(o.status.academicStatus);
            if (result != 0) {
                return result;
            }
            result = status.date.compareTo(o.status.date);
            if (result != 0) {
                return result;
            }
            return 0;
        }

        public static class Faculty {

            protected String department;
            protected String section;

            public String getDepartment() {
                return department;
            }

            public void setDepartment(String value) {
                this.department = value;
            }

            public String getSection() {
                return section;
            }

            public void setSection(String value) {
                this.section = value;
            }

        }

        public static class Status {

            protected String academicStatus;
            protected String date;

            public String getAcademicStatus() {
                return academicStatus;
            }

            public void setAcademicStatus(String value) {
                this.academicStatus = value;
            }

            public String getDate() {
                return date;
            }

            public void setDate(String value) {
                this.date = value;
            }

        }

    }

}
