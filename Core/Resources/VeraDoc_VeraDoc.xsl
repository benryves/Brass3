<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/file">
	<html>
	<head>
		<title>Documentation of <xsl:value-of select="name"/></title>
		<link href="veradoc.css" rel="stylesheet" type="text/css"/>
	</head>
	<body>
		<h1>Documentation of source file <xsl:value-of select="name"/></h1>
		<xsl:if test="description/p">
			<h2>File description</h2>
			<xsl:for-each select="description/p">
				<p><xsl:value-of select="."/></p>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="routine">
			<h2>Overview of routines</h2>
			<ul>
			<xsl:for-each select="routine">
			<xsl:sort select="name"/>
				<xsl:variable name="link" select="name"/>
				<li><a href="#{$link}"><xsl:value-of select="name"/></a></li>
			</xsl:for-each>
			</ul>
		</xsl:if>
		<xsl:for-each select="routine">
			<xsl:variable name="link" select="name"/>
			<a name="{$link}"/><h2>Routine <xsl:value-of select="name"/></h2>
			<xsl:if test="description">
				<h3>Description</h3>
				<xsl:for-each select="description/p">
					<p><xsl:value-of select="."/></p>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="precondition">
				<h3>Preconditions</h3>
				<ul>
				<xsl:for-each select="precondition">
					<li><xsl:value-of select="."/></li>
				</xsl:for-each>
				</ul>
			</xsl:if>
			<xsl:if test="postcondition">
				<h3>Postconditions</h3>
				<ul>
				<xsl:for-each select="postcondition">
					<li><xsl:value-of select="."/></li>
				</xsl:for-each>
				</ul>
			</xsl:if>
			<xsl:if test="seealso">
				<h3>See also</h3>
				<ul>
				<xsl:for-each select="seealso">
					<li>
						<xsl:variable name="seealsoname" select="."/>
						<xsl:variable name="isfound">
							<xsl:for-each select="/file/routine/name">
								<xsl:variable name="routinename" select="."/>
								<xsl:if test="$routinename=$seealsoname">
									<xsl:value-of select="true()"/>
								</xsl:if>
							</xsl:for-each>
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$isfound='true'">
								<a href="#{$seealsoname}"><xsl:value-of select="."/></a>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="."/>
							</xsl:otherwise>
						</xsl:choose>
					</li>
				</xsl:for-each>
				</ul>
			</xsl:if>
			<xsl:if test="warning">
				<h3 class="warning">Warning</h3>
				<xsl:for-each select="warning/p">
					<p><xsl:value-of select="."/></p>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="example">
				<h3>Example</h3>
				<pre><xsl:for-each select="example/line">
					<xsl:value-of select="."/><xsl:text>&#10;</xsl:text>
				</xsl:for-each></pre>
			</xsl:if>
		</xsl:for-each>
		<br/>
	</body>
	</html>
</xsl:template>

<xsl:template match="/tree">
	<html>
	<head>
		<title>Source Tree</title>
		<link href="veradoc.css" rel="stylesheet" type="text/css"/>
	</head>
	<body>
		<h1>Source Tree</h1>
		<ul class="tree">
			<xsl:apply-templates/>
		</ul>
	</body>
	</html>
</xsl:template>

<xsl:template match="dir/name">
</xsl:template>

<xsl:template match="dir">
	<li class="dir"><xsl:value-of select="name"/>/
		<ul>
			<xsl:apply-templates/>
		</ul>
	</li>
</xsl:template>

<xsl:template match="treefile">
	<xsl:variable name="link" select="file"/>
	<li class="file"><a href="{$link}" target="file"><xsl:value-of select="name"/></a></li>
</xsl:template>

</xsl:stylesheet>

