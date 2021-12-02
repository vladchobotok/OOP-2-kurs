package xmlclasses;

public class ObjectFactory {


    public ObjectFactory() {
    }

    public Scientists createScientists() {
        return new Scientists();
    }

    public Scientists.Scientist createScientistsScientist() {
        return new Scientists.Scientist();
    }
    public Scientists.Scientist createScientistsScientist(String surname, String name, String middleName,
                     Scientists.Scientist.Faculty faculty,
                     String cathedra, String degree,
                     Scientists.Scientist.Status status){
        return new Scientists.Scientist(surname, name, middleName,
                faculty,
                cathedra, degree,
                status);
    }

    public Scientists.Scientist.Faculty createScientistsScientistFaculty() {
        return new Scientists.Scientist.Faculty();
    }

    public Scientists.Scientist.Status createScientistsScientistStatus() {
        return new Scientists.Scientist.Status();
    }

}
