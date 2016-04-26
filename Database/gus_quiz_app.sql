-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: localhost:3306
-- Generation Time: Apr 26, 2016 at 04:12 PM
-- Server version: 5.1.73-community
-- PHP Version: 5.5.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `gus_quiz_app`
--

-- --------------------------------------------------------

--
-- Table structure for table `course`
--

CREATE TABLE `course` (
  `ID` int(11) NOT NULL,
  `Name` varchar(45) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `course`
--

INSERT INTO `course` (`ID`, `Name`) VALUES
(1, 'Software Developer'),
(2, 'Web Developer'),
(3, 'Business'),
(4, 'Design Graphic');

-- --------------------------------------------------------

--
-- Table structure for table `lecturer`
--

CREATE TABLE `lecturer` (
  `ID` int(11) NOT NULL,
  `LecturerNumber` int(11) NOT NULL,
  `FirstName` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `LastName` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `Email` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `Password` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `IsAdm` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `lecturer`
--

INSERT INTO `lecturer` (`ID`, `LecturerNumber`, `FirstName`, `LastName`, `Email`, `Password`, `IsAdm`) VALUES
(1, 4123123, 'Admin', 'admin', 'admin@mail.com', '123', 1),
(2, 4123456, 'Not', 'Admin', 'admin@mail.com', '123', 0);

-- --------------------------------------------------------

--
-- Table structure for table `optionanswer`
--

CREATE TABLE `optionanswer` (
  `ID` int(11) NOT NULL,
  `Option` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `QuestionID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `question`
--

CREATE TABLE `question` (
  `QuestionID` int(11) NOT NULL,
  `Title` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `CorrectAnswer` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `TypeQuestionID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `question`
--

INSERT INTO `question` (`QuestionID`, `Title`, `CorrectAnswer`, `TypeQuestionID`) VALUES
(2, 'Question 1', 'True', 1),
(3, 'Question 2', 'JAVA', 4);

-- --------------------------------------------------------

--
-- Table structure for table `questionnaire`
--

CREATE TABLE `questionnaire` (
  `ID` int(11) NOT NULL,
  `Name` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `EndDate` datetime NOT NULL,
  `UnitID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `questionnaire`
--

INSERT INTO `questionnaire` (`ID`, `Name`, `EndDate`, `UnitID`) VALUES
(1, 'Quiz 1', '2015-06-12 00:00:00', 3);

-- --------------------------------------------------------

--
-- Table structure for table `questionnairequestion`
--

CREATE TABLE `questionnairequestion` (
  `QuestionID` int(11) NOT NULL,
  `QuestionnaireID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `student`
--

CREATE TABLE `student` (
  `StudentID` int(11) NOT NULL,
  `StudentNumber` int(11) NOT NULL,
  `FirstName` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastName` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Email` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Password` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CourseID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `student`
--

INSERT INTO `student` (`StudentID`, `StudentNumber`, `FirstName`, `LastName`, `Email`, `Password`, `CourseID`) VALUES
(1, 1, 'Connie', 'Qi', 'connie.qi@mail.com', 'admin', 2),
(2, 414444444, 'Helvio', 'Carvalho', 'admin@admin.com', '123', 2),
(3, 4123123, 'Bonzo', 'Kaldaris', 'yo@bobo.com', 'password', 2);

-- --------------------------------------------------------

--
-- Table structure for table `typequestion`
--

CREATE TABLE `typequestion` (
  `ID` int(11) NOT NULL,
  `Type` varchar(45) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `typequestion`
--

INSERT INTO `typequestion` (`ID`, `Type`) VALUES
(1, 'Boolean'),
(2, 'Multiple Choice'),
(3, 'Single Choice'),
(4, 'Text');

-- --------------------------------------------------------

--
-- Table structure for table `unit`
--

CREATE TABLE `unit` (
  `ID` int(11) NOT NULL,
  `Name` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `CourseID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `unit`
--

INSERT INTO `unit` (`ID`, `Name`, `CourseID`) VALUES
(1, 'Java', 1),
(2, 'C#', 1),
(3, 'Android', 1),
(4, 'HTML5', 2),
(5, 'PHP', 2);

-- --------------------------------------------------------

--
-- Table structure for table `unitlecturer`
--

CREATE TABLE `unitlecturer` (
  `UnitID` int(11) NOT NULL DEFAULT '0',
  `LecturerID` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `unitlecturer`
--

INSERT INTO `unitlecturer` (`UnitID`, `LecturerID`) VALUES
(2, 1),
(1, 2),
(3, 2),
(4, 2);

-- --------------------------------------------------------

--
-- Table structure for table `useranswer`
--

CREATE TABLE `useranswer` (
  `StudentID` int(11) NOT NULL DEFAULT '0',
  `QuestionID` int(11) NOT NULL DEFAULT '0',
  `Answer` varchar(128) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `course`
--
ALTER TABLE `course`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `lecturer`
--
ALTER TABLE `lecturer`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `optionanswer`
--
ALTER TABLE `optionanswer`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `QuestionID_idx` (`QuestionID`) USING BTREE;

--
-- Indexes for table `question`
--
ALTER TABLE `question`
  ADD PRIMARY KEY (`QuestionID`),
  ADD KEY `TypeQuestionPK_idx` (`TypeQuestionID`) USING BTREE;

--
-- Indexes for table `questionnaire`
--
ALTER TABLE `questionnaire`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `UnitPK_idx` (`UnitID`) USING BTREE;

--
-- Indexes for table `questionnairequestion`
--
ALTER TABLE `questionnairequestion`
  ADD KEY `IDQuestionnaire_idx` (`QuestionnaireID`) USING BTREE,
  ADD KEY `QuestionnairePK_idx` (`QuestionnaireID`) USING BTREE,
  ADD KEY `IDQuestion` (`QuestionID`);

--
-- Indexes for table `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`StudentID`),
  ADD UNIQUE KEY `index_student_on_email` (`Email`),
  ADD KEY `CourseID_idx` (`CourseID`) USING BTREE;

--
-- Indexes for table `typequestion`
--
ALTER TABLE `typequestion`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `unit`
--
ALTER TABLE `unit`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `CourseID` (`CourseID`);

--
-- Indexes for table `unitlecturer`
--
ALTER TABLE `unitlecturer`
  ADD PRIMARY KEY (`UnitID`,`LecturerID`),
  ADD KEY `lectID` (`LecturerID`);

--
-- Indexes for table `useranswer`
--
ALTER TABLE `useranswer`
  ADD PRIMARY KEY (`StudentID`,`QuestionID`),
  ADD KEY `QuesID` (`QuestionID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `course`
--
ALTER TABLE `course`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `lecturer`
--
ALTER TABLE `lecturer`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `optionanswer`
--
ALTER TABLE `optionanswer`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `question`
--
ALTER TABLE `question`
  MODIFY `QuestionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `questionnaire`
--
ALTER TABLE `questionnaire`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `student`
--
ALTER TABLE `student`
  MODIFY `StudentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `typequestion`
--
ALTER TABLE `typequestion`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `unit`
--
ALTER TABLE `unit`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `optionanswer`
--
ALTER TABLE `optionanswer`
  ADD CONSTRAINT `QuestionPK` FOREIGN KEY (`QuestionID`) REFERENCES `question` (`QuestionID`);

--
-- Constraints for table `question`
--
ALTER TABLE `question`
  ADD CONSTRAINT `TypeQuestionPK` FOREIGN KEY (`TypeQuestionID`) REFERENCES `typequestion` (`ID`);

--
-- Constraints for table `questionnaire`
--
ALTER TABLE `questionnaire`
  ADD CONSTRAINT `UnitPK` FOREIGN KEY (`UnitID`) REFERENCES `unit` (`ID`);

--
-- Constraints for table `questionnairequestion`
--
ALTER TABLE `questionnairequestion`
  ADD CONSTRAINT `IDQuestion` FOREIGN KEY (`QuestionID`) REFERENCES `question` (`QuestionID`),
  ADD CONSTRAINT `IDQuestionnaire` FOREIGN KEY (`QuestionnaireID`) REFERENCES `questionnaire` (`ID`);

--
-- Constraints for table `student`
--
ALTER TABLE `student`
  ADD CONSTRAINT `IdCourse` FOREIGN KEY (`CourseID`) REFERENCES `course` (`ID`);

--
-- Constraints for table `unit`
--
ALTER TABLE `unit`
  ADD CONSTRAINT `CourseID` FOREIGN KEY (`CourseID`) REFERENCES `course` (`ID`);

--
-- Constraints for table `unitlecturer`
--
ALTER TABLE `unitlecturer`
  ADD CONSTRAINT `lectID` FOREIGN KEY (`LecturerID`) REFERENCES `lecturer` (`ID`),
  ADD CONSTRAINT `Unit_ID` FOREIGN KEY (`UnitID`) REFERENCES `unit` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `useranswer`
--
ALTER TABLE `useranswer`
  ADD CONSTRAINT `QuesID` FOREIGN KEY (`QuestionID`) REFERENCES `question` (`QuestionID`),
  ADD CONSTRAINT `stuID` FOREIGN KEY (`StudentID`) REFERENCES `student` (`StudentID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
