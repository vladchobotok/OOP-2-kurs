package com.company.parsers;

import com.company.Consts;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;
import xmlclasses.Scientists.Scientist;

import javax.xml.XMLConstants;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

public class DOMParser {

    private final String filename;

    private final List<Scientist> scientists;

    public DOMParser(String filename) {
        scientists = new ArrayList<>();
        this.filename = filename;
    }

    public List<Scientist> getScientists() {
        return scientists;
    }


    public void parseXML(boolean validate){

        DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();

        try {

            dbf.setFeature(XMLConstants.FEATURE_SECURE_PROCESSING, true);
            dbf.setFeature("http://apache.org/xml/features/disallow-doctype-decl", true);

            DocumentBuilder db = dbf.newDocumentBuilder();

            dbf.setNamespaceAware(true);
            if (validate) {
                        dbf.setFeature(Consts.FEATURE_TURN_VALIDATION_ON, true);
                        dbf.setFeature(Consts.FEATURE_TURN_SCHEMA_VALIDATION_ON, true);
            }

            Document doc = db.parse(new File(filename));

            doc.getDocumentElement().normalize();

            NodeList list = doc.getElementsByTagName(Consts.SCIENTIST);

            for (int temp = 0; temp < list.getLength(); temp++) {

                Node node = list.item(temp);
                Scientist scientist;

                if (node.getNodeType() == Node.ELEMENT_NODE) {

                    Element element = (Element) node;

                    String surname = element.getElementsByTagName(Consts.SURNAME).item(0).getTextContent();
                    String name = element.getElementsByTagName(Consts.NAME).item(0).getTextContent();
                    String middleName = element.getElementsByTagName(Consts.MIDDLENAME).item(0).getTextContent();

                    NodeList faculty = element.getElementsByTagName(Consts.FACULTY);
                    Scientist.Faculty faculty1 = getFaculty(faculty);

                    String cathedra = element.getElementsByTagName(Consts.CATHEDRA).item(0).getTextContent();
                    String degree = element.getElementsByTagName(Consts.DEGREE).item(0).getTextContent();

                    NodeList status = element.getElementsByTagName(Consts.STATUS);
                    Scientist.Status status1 = getStatus(status);

                    scientist = new Scientist(surname, name, middleName, faculty1, cathedra, degree, status1);
                    scientists.add(scientist);
                }
            }

        } catch (ParserConfigurationException | SAXException | IOException e) {
            Logger.getLogger(DOMParser.class.getName()).log(Level.SEVERE, Consts.ERROR, e);
        }
    }

    private Scientist.Faculty getFaculty(NodeList faculty){
        Scientist.Faculty faculty1 = new Scientist.Faculty();
        for (int tmp = 0; tmp < faculty.getLength(); tmp++) {

            Node node1 = faculty.item(tmp);
            if (node1.getNodeType() == Node.ELEMENT_NODE) {
                Element element1 = (Element) node1;
                faculty1.setDepartment(element1.getElementsByTagName(Consts.DEPARTMENT).item(0).getTextContent());
                faculty1.setSection(element1.getElementsByTagName(Consts.SECTION).item(0).getTextContent());
            }
        }
        return faculty1;
    }

    private Scientist.Status getStatus(NodeList status){
        Scientist.Status status1 = new Scientist.Status();

        for (int tmp = 0; tmp < status.getLength(); tmp++) {

            Node node1 = status.item(tmp);

            if (node1.getNodeType() == Node.ELEMENT_NODE) {

                Element element1 = (Element) node1;
                status1.setAcademicStatus(element1.getElementsByTagName(Consts.ACADEMICSTATUS).item(0).getTextContent());
                status1.setDate(element1.getElementsByTagName(Consts.DATE).item(0).getTextContent());
            }
        }
        return status1;
    }

    public void sortXML(){
        Collections.sort(scientists);
    }

    private Document createDocument() throws ParserConfigurationException {
        final String[] scientistsArray = { Consts.SURNAME, Consts.NAME, Consts.MIDDLENAME, Consts.FACULTY, Consts.CATHEDRA, Consts.DEGREE, Consts.STATUS};
        final String[] facultyArray = { Consts.DEPARTMENT, Consts.SECTION};
        final String[] statusArray = { Consts.ACADEMICSTATUS, Consts.DATE };
        DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
        dbf.setAttribute(XMLConstants.FEATURE_SECURE_PROCESSING, true);
        dbf.setAttribute(XMLConstants.ACCESS_EXTERNAL_DTD, "");
        dbf.setAttribute(XMLConstants.ACCESS_EXTERNAL_SCHEMA, "");
        dbf.setFeature("http://apache.org/xml/features/disallow-doctype-decl", true);
        dbf.setFeature("http://xml.org/sax/features/external-general-entities", false);
        dbf.setFeature("http://xml.org/sax/features/external-parameter-entities", false);
        dbf.setFeature("http://apache.org/xml/features/nonvalidating/load-external-dtd", false);
        dbf.setXIncludeAware(false);
        dbf.setExpandEntityReferences(false);
        dbf.setNamespaceAware(true);
        DocumentBuilder documentBuilder = dbf.newDocumentBuilder();
        Document document = documentBuilder.newDocument();
        Element root = document.createElement(Consts.SCIENTISTS);
        document.appendChild(root);
        Element element;
        for (Scientist scientist : scientists) {
            Element scientistElement = document.createElement(Consts.SCIENTIST);
            List<Object> listedScientists = scientistToList(scientist);

            for (int i = 0; i < 3; i++) {
                element = document.createElement(scientistsArray[i]);
                element.setTextContent(listedScientists.get(i).toString());
                scientistElement.appendChild(element);
            }

            Element facultyElement = document.createElement(Consts.FACULTY);

            Element childElement;

            childElement = document.createElement(facultyArray[0]);
            childElement.setTextContent(scientist.getFaculty().getDepartment());
            facultyElement.appendChild(childElement);

            childElement = document.createElement(facultyArray[1]);
            childElement.setTextContent(scientist.getFaculty().getSection());
            facultyElement.appendChild(childElement);

            scientistElement.appendChild(facultyElement);

            for (int i = 4; i < 6; i++) {
                element = document.createElement(scientistsArray[i]);
                element.setTextContent(listedScientists.get(i).toString());
                scientistElement.appendChild(element);
            }

            Element statusElement = document.createElement(Consts.STATUS);

            childElement = document.createElement(statusArray[0]);
            childElement.setTextContent(scientist.getStatus().getAcademicStatus());
            statusElement.appendChild(childElement);

            childElement = document.createElement(statusArray[1]);
            childElement.setTextContent(scientist.getStatus().getDate());
            statusElement.appendChild(childElement);

            scientistElement.appendChild(statusElement);

            root.appendChild(scientistElement);
        }
        return document;

    }

    public void saveToXML(){

        StreamResult result = new StreamResult(new File(Consts.DOM_RESULT));

        try {
            TransformerFactory tf = TransformerFactory.newInstance();
            tf.setAttribute(XMLConstants.ACCESS_EXTERNAL_DTD, "");
            tf.setAttribute(XMLConstants.ACCESS_EXTERNAL_STYLESHEET, "");
            javax.xml.transform.Transformer t = null;
            t = tf.newTransformer();
            t.setOutputProperty(OutputKeys.INDENT, "yes");
            t.transform(new DOMSource(createDocument()), result);
        } catch (ParserConfigurationException | TransformerException e) {
            Logger.getLogger(DOMParser.class.getName()).log(Level.SEVERE, Consts.ERROR, e);
        }

    }

    public static List<Object> scientistToList(Scientist scientist) {
        List<Object> resultList = new ArrayList<>();
        resultList.add(scientist.getSurname());
        resultList.add(scientist.getName());
        resultList.add(scientist.getMiddleName());
        resultList.add(scientist.getFaculty());
        resultList.add(scientist.getCathedra());
        resultList.add(scientist.getDegree());
        resultList.add(scientist.getStatus());
        return resultList;
    }
}