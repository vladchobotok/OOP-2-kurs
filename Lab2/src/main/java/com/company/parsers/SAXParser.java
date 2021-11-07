package com.company.parsers;

import java.io.FileWriter;
import java.io.IOException;
import java.util.*;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.xml.parsers.ParserConfigurationException;
import javax.xml.parsers.SAXParserFactory;
import javax.xml.stream.XMLOutputFactory;
import javax.xml.stream.XMLStreamException;
import javax.xml.stream.XMLStreamWriter;

import com.company.Consts;
import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.SAXNotRecognizedException;
import org.xml.sax.SAXNotSupportedException;
import org.xml.sax.helpers.DefaultHandler;
import xmlclasses.Scientists.Scientist;

public class SAXParser extends DefaultHandler {

    private String filename;

    private List<Scientist> scientists;
    private Deque<Object> elementDeque;
    private Deque<Scientist> objectDeque;
    private Deque<Scientist.Faculty> fObjectDeque;
    private Deque<Scientist.Status> sObjectDeque;

    public SAXParser(String filename) {
        scientists = new ArrayList<>(20);
        elementDeque = new ArrayDeque<>();
        objectDeque = new ArrayDeque<>();
        fObjectDeque = new ArrayDeque<>();
        sObjectDeque = new ArrayDeque<>();
        this.filename = filename;
    }

    public List<Scientist> getScientists() {
        return scientists;
    }

    public void parse(boolean validate) {
        SAXParserFactory factory = SAXParserFactory.newInstance();
        factory.setNamespaceAware(true);
        try {
            factory.setFeature("http://apache.org/xml/features/disallow-doctype-decl", true);
        } catch (ParserConfigurationException | SAXNotRecognizedException | SAXNotSupportedException e) {
            Logger.getLogger(DOMParser.class.getName()).log(Level.SEVERE, Consts.ERROR, e);
        }

        if (validate) {
            factory.setValidating(true);
            try {
                factory.setFeature(Consts.FEATURE_TURN_VALIDATION_ON, true);
                factory.setFeature(Consts.FEATURE_TURN_SCHEMA_VALIDATION_ON, true);
            } catch (SAXNotRecognizedException | SAXNotSupportedException | ParserConfigurationException e) {
                Logger.getLogger(DOMParser.class.getName()).log(Level.SEVERE, Consts.ERROR, e);
            }
        }
        javax.xml.parsers.SAXParser parser;
        try {
            parser = factory.newSAXParser();
            parser.parse(filename, this);
        } catch (SAXException | IOException | ParserConfigurationException e) {
            Logger.getLogger(DOMParser.class.getName()).log(Level.SEVERE, Consts.ERROR, e);
        }
    }

    public void sortXML(){
        Collections.sort(scientists);
    }

    @Override
    public void startElement(String uri, String localName, String qName, Attributes attributes){

        this.elementDeque.push(qName);

        if (Consts.SCIENTIST.equals(qName))
        {
            Scientist scientist = new Scientist();
            this.objectDeque.push(scientist);
        }
        else if (Consts.FACULTY.equals(qName))
        {
            Scientist.Faculty visualParameters1 = new Scientist.Faculty();
            this.fObjectDeque.push(visualParameters1);
        }
        else if (Consts.STATUS.equals(qName))
        {
            Scientist.Status growingTips1 = new Scientist.Status();
            this.sObjectDeque.push(growingTips1);
        }
    }

    @Override
    public void endElement(String uri, String localName, String qName){

        this.elementDeque.pop();

        if (Consts.SCIENTIST.equals(qName))
        {
            Scientist object = this.objectDeque.pop();
            this.scientists.add(object);
        }
        else if (Consts.FACULTY.equals(qName))
        {
            Scientist scientist = this.objectDeque.peek();
            Scientist.Faculty object = this.fObjectDeque.pop();
            scientist.setFaculty(object);

        }
        else if (Consts.STATUS.equals(qName))
        {
            Scientist scientist = this.objectDeque.peek();
            Scientist.Status object = this.sObjectDeque.pop();
            scientist.setStatus(object);
        }
    }

    @Override
    public void characters(char[] ch, int start, int length){
        String value = new String(ch, start, length).trim();
        if (value.length() == 0) {
            return;
        }

        Scientist scientist = this.objectDeque.peek();

        if (Consts.SURNAME.equals(currentElement())) {
            scientist.setSurname(value);
        }
        else if (Consts.NAME.equals(currentElement())) {
            scientist.setName(value);
        }
        else if (Consts.MIDDLENAME.equals(currentElement())){
            scientist.setMiddleName(value);
        }
        else if(Consts.CATHEDRA.equals(currentElement())){
            scientist.setCathedra(value);
        }
        else if(Consts.DEGREE.equals(currentElement())){
            scientist.setDegree(value);
        }
        else if(Consts.DEPARTMENT.equals(currentElement())){
            Scientist.Faculty faculty = this.fObjectDeque.peek();
            faculty.setDepartment(value);
        }
        else if(Consts.SECTION.equals(currentElement())){
            Scientist.Faculty faculty = this.fObjectDeque.peek();
            faculty.setSection(value);
        }
        else if(Consts.ACADEMICSTATUS.equals(currentElement())){
            Scientist.Status status = this.sObjectDeque.peek();
            status.setAcademicStatus(value);
        }
        else if(Consts.DATE.equals(currentElement())){
            Scientist.Status status = this.sObjectDeque.peek();
            status.setDate(value);
        }
    }

    private Object currentElement()
    {
        return this.elementDeque.peek();
    }

    public void writeXML(){
        try {
            writeSaxAndStax(scientists, Consts.SAX_RESULT);
        } catch (IOException | XMLStreamException e) {
            Logger.getLogger(DOMParser.class.getName()).log(Level.SEVERE, Consts.ERROR, e);
        }
    }

    public static void writeSaxAndStax(List<Scientist> scientists, String path) throws IOException, XMLStreamException {
        final String[] scientistsArray = { Consts.SURNAME, Consts.NAME, Consts.MIDDLENAME, Consts.FACULTY, Consts.CATHEDRA, Consts.DEGREE, Consts.STATUS};
        final String[] facultyArray = { Consts.DEPARTMENT, Consts.SECTION};
        final String[] statusArray = { Consts.ACADEMICSTATUS, Consts.DATE };
        XMLOutputFactory outputFactory = XMLOutputFactory.newInstance();
        XMLStreamWriter xmlStreamWriter;
        try (FileWriter fileWriter = new FileWriter(path)) {
            xmlStreamWriter = outputFactory.createXMLStreamWriter(fileWriter);
            xmlStreamWriter.writeStartDocument();

            xmlStreamWriter.writeStartElement(Consts.SCIENTISTS);

            for (Scientist scientist : scientists) {
                List<Object> objectsScientist = DOMParser.scientistToList(scientist);
                xmlStreamWriter.writeStartElement(Consts.SCIENTIST);

                for (int i = 0; i < 3; i++) {
                    xmlStreamWriter.writeStartElement(scientistsArray[i]);
                    xmlStreamWriter.writeCharacters(objectsScientist.get(i).toString());
                    xmlStreamWriter.writeEndElement();
                }

                xmlStreamWriter.writeStartElement(Consts.FACULTY);

                xmlStreamWriter.writeStartElement(facultyArray[0]);
                xmlStreamWriter.writeCharacters(scientist.getFaculty().getDepartment());
                xmlStreamWriter.writeEndElement();

                xmlStreamWriter.writeStartElement(facultyArray[1]);
                xmlStreamWriter.writeCharacters(scientist.getFaculty().getSection());
                xmlStreamWriter.writeEndElement();

                xmlStreamWriter.writeEndElement();

                for (int i = 4; i < 6; i++) {
                    xmlStreamWriter.writeStartElement(scientistsArray[i]);
                    xmlStreamWriter.writeCharacters(objectsScientist.get(i).toString());
                    xmlStreamWriter.writeEndElement();
                }

                xmlStreamWriter.writeStartElement(Consts.STATUS);

                xmlStreamWriter.writeStartElement(statusArray[0]);
                xmlStreamWriter.writeCharacters(scientist.getStatus().getAcademicStatus());
                xmlStreamWriter.writeEndElement();

                xmlStreamWriter.writeStartElement(statusArray[1]);
                xmlStreamWriter.writeCharacters(scientist.getStatus().getDate());
                xmlStreamWriter.writeEndElement();

                xmlStreamWriter.writeEndElement();
                xmlStreamWriter.writeEndElement();
            }
            xmlStreamWriter.writeEndDocument();
        }
    }
}
