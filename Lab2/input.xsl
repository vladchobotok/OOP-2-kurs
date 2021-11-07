<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" version="4.0" encoding="UTF-8" indent="yes" />
    <xsl:template match="/">
        <html>
            <head>
                <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
            </head>
            <body>
                <h2>Кадри науковців(Звання)</h2>
                <table border="1">
                    <tr>
                        <th>Призвіще</th>
                        <th>Ім'я</th>
                        <th>По батькові</th>
                        <th>Департамент</th>
                        <th>Відділення</th>
                        <th>Кафедра</th>
                        <th>Науковий ступінь</th>
                        <th>Вчене звання</th>
                        <th>Дата</th>
                    </tr>
                    <xsl:for-each select="scientists/scientist">
                        <tr>
                            <td><xsl:value-of select="surname"/></td>
                            <td><xsl:value-of select="name"/></td>
                            <td><xsl:value-of select="middleName"/></td>
                            <xsl:for-each select="faculty">
                                <td><xsl:value-of select="department"/></td>
                                <td><xsl:value-of select="section"/></td>
                            </xsl:for-each>
                            <td><xsl:value-of select="cathedra"/></td>
                            <td><xsl:value-of select="degree"/></td>
                            <xsl:for-each select="status">
                                <td><xsl:value-of select="academicStatus"/></td>
                                <td><xsl:value-of select="date"/></td>
                            </xsl:for-each>
                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>