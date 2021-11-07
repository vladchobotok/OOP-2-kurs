package com.company.parsers;

import javax.xml.transform.*;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.OutputStream;

public class HTMLConverter {
    public void convertToHtml(String xsl, String xml, String html){
        try {
            TransformerFactory tFactory=TransformerFactory.newInstance();

            Source xslDoc = new StreamSource(xsl);
            Source xmlDoc = new StreamSource(xml);

            String outputFileName = html;

            OutputStream htmlFile = new FileOutputStream(outputFileName);
            Transformer transformer = tFactory.newTransformer(xslDoc);
            transformer.transform(xmlDoc, new StreamResult(htmlFile));
        } catch (FileNotFoundException | TransformerFactoryConfigurationError | TransformerException e)
        {
            e.printStackTrace();
        }
    }
}
