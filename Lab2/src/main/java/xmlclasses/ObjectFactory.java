
package xmlclasses;

import javax.xml.bind.annotation.XmlRegistry;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the xmlclasses package. 
 * <p>An ObjectFactory allows you to programatically 
 * construct new instances of the Java representation 
 * for XML content. The Java representation of XML 
 * content can consist of schema derived interfaces 
 * and classes representing the binding of schema 
 * type definitions, element declarations and model 
 * groups.  Factory methods for each of these are 
 * provided in this class.
 * 
 */
@XmlRegistry
public class ObjectFactory {


    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: xmlclasses
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link Scientists }
     * 
     */
    public Scientists createScientists() {
        return new Scientists();
    }

    /**
     * Create an instance of {@link Scientists.Scientist }
     * 
     */
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

    /**
     * Create an instance of {@link Scientists.Scientist.Faculty }
     * 
     */
    public Scientists.Scientist.Faculty createScientistsScientistFaculty() {
        return new Scientists.Scientist.Faculty();
    }

    /**
     * Create an instance of {@link Scientists.Scientist.Status }
     * 
     */
    public Scientists.Scientist.Status createScientistsScientistStatus() {
        return new Scientists.Scientist.Status();
    }

}
