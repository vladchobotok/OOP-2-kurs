package com.company.parsers;

import com.company.Consts;
import xmlclasses.ObjectFactory;
import xmlclasses.Scientists.Scientist;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.xml.stream.XMLEventReader;
import javax.xml.stream.XMLInputFactory;
import javax.xml.stream.XMLStreamException;
import javax.xml.stream.events.EndElement;
import javax.xml.stream.events.StartElement;
import javax.xml.stream.events.XMLEvent;
import javax.xml.transform.stream.StreamSource;


public class StAXParser {

    private String filename;
    private ObjectFactory objectFactory;

    private List<Scientist> scientists;
    Scientist scientist;
    Scientist.Faculty faculty;
    Scientist.Status status;

    public StAXParser(String filename) {
        objectFactory = new ObjectFactory();
        scientists = new ArrayList<>();
        this.filename = filename;
    }

    public List<Scientist> getScientists() {
        return scientists;
    }


    public void parse() {
        XMLInputFactory factory = XMLInputFactory.newInstance();
        factory.setProperty(XMLInputFactory.SUPPORT_DTD, false);
        try {
            XMLEventReader eventReader = factory.createXMLEventReader(new StreamSource(filename));
            while (eventReader.hasNext()) {
                XMLEvent xmlEvent = eventReader.nextEvent();
                readInput(xmlEvent,eventReader);
            }
        } catch (XMLStreamException e) {
            Logger.getLogger(DOMParser.class.getName()).log(Level.SEVERE, Consts.ERROR, e);
        }
    }

    public void sortXML(){
        Collections.sort(scientists);
    }

    private void readInput(XMLEvent xmlEvent, XMLEventReader eventReader) throws XMLStreamException {

        if (xmlEvent.isStartElement()) {
            StartElement startElement = xmlEvent.asStartElement();
            if (startElement.getName().getLocalPart().equals(Consts.SCIENTIST)) {
                scientist = objectFactory.createScientistsScientist();
            }
            else if (startElement.getName().getLocalPart().equals(Consts.SURNAME)) {
                xmlEvent = eventReader.nextEvent();
                scientist.setSurname(xmlEvent.asCharacters().getData());
            }
            else if (startElement.getName().getLocalPart().equals(Consts.NAME)) {
                xmlEvent = eventReader.nextEvent();
                scientist.setName(xmlEvent.asCharacters().getData());
            }
            else if (startElement.getName().getLocalPart().equals(Consts.MIDDLENAME)) {
                xmlEvent = eventReader.nextEvent();
                scientist.setMiddleName(xmlEvent.asCharacters().getData());
            }
            else if (startElement.getName().getLocalPart().equals(Consts.FACULTY)) {
                faculty = objectFactory.createScientistsScientistFaculty();
                setFaculty(eventReader);
            }
            else if (startElement.getName().getLocalPart().equals(Consts.CATHEDRA)) {
                xmlEvent = eventReader.nextEvent();
                scientist.setCathedra(xmlEvent.asCharacters().getData());
            }
            else if (startElement.getName().getLocalPart().equals(Consts.DEGREE)) {
                xmlEvent = eventReader.nextEvent();
                scientist.setDegree(xmlEvent.asCharacters().getData());
            }
            else if (startElement.getName().getLocalPart().equals(Consts.STATUS)) {
                status = objectFactory.createScientistsScientistStatus();
                setStatus(eventReader);
            }
        } else if (xmlEvent.isEndElement()) {
            EndElement endElement = xmlEvent.asEndElement();
            if (endElement.getName().getLocalPart().equals(Consts.SCIENTIST)) {
                scientists.add(scientist);
            } else if (endElement.getName().getLocalPart().equals(Consts.FACULTY)) {
                scientist.setFaculty(faculty);
            } else if (endElement.getName().getLocalPart().equals(Consts.STATUS)) {
                scientist.setStatus(status);
            }
        }
    }

    private void setFaculty(XMLEventReader reader) {
        XMLEvent event;
        StartElement element;
        for (int i = 0; i < 6; i++) {
            try {
                event = reader.nextEvent();
                if (event.isStartElement()) {
                    element = event.asStartElement();
                    if (element.getName().getLocalPart().equals(Consts.DEPARTMENT)) {
                        event = reader.nextEvent();
                        faculty.setDepartment(event.asCharacters().getData());
                    }
                    else if (element.getName().getLocalPart().equals(Consts.SECTION)) {
                        event = reader.nextEvent();
                        faculty.setSection(event.asCharacters().getData());
                    }
                }
            } catch (XMLStreamException e) {
                Logger.getLogger(DOMParser.class.getName()).log(Level.SEVERE, Consts.ERROR, e);
            }
        }
    }

    private void setStatus(XMLEventReader reader) {
        XMLEvent event;
        StartElement element;
        for (int i = 0; i < 6; i++) {
            try {
                event = reader.nextEvent();
                if (event.isStartElement()) {
                    element = event.asStartElement();
                    if (element.getName().getLocalPart().equals(Consts.ACADEMICSTATUS)) {
                        event = reader.nextEvent();
                        status.setAcademicStatus(event.asCharacters().getData());
                    }
                    else if (element.getName().getLocalPart().equals(Consts.DATE)) {
                        event = reader.nextEvent();
                        status.setDate(event.asCharacters().getData());
                    }
                }
            } catch (XMLStreamException e) {
                Logger.getLogger(DOMParser.class.getName()).log(Level.SEVERE, Consts.ERROR, e);
            }
        }
    }

    public void writeXML() {
        try {
            SAXParser.writeSaxAndStax(scientists, Consts.STAX_RESULT);
        } catch (IOException | XMLStreamException e) {
            Logger.getLogger(DOMParser.class.getName()).log(Level.SEVERE, Consts.ERROR, e);
        }
    }
}