-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 13, 2018 at 03:40 PM
-- Server version: 10.1.26-MariaDB
-- PHP Version: 7.1.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `security_system`
--

-- --------------------------------------------------------

--
-- Table structure for table `city`
--

CREATE TABLE `city` (
  `id` int(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `countryid` int(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `city`
--

INSERT INTO `city` (`id`, `name`, `countryid`) VALUES
(1, 'Lahore', 1),
(2, 'Karachi', 1),
(3, 'Islamabad', 1),
(4, 'Mumbai', 2),
(5, 'Dehli', 2),
(6, 'Amritsar', 2),
(7, 'Beijing', 3),
(8, 'Shanghai', 3);

-- --------------------------------------------------------

--
-- Table structure for table `country`
--

CREATE TABLE `country` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `country`
--

INSERT INTO `country` (`id`, `name`) VALUES
(1, 'Pakistan'),
(2, 'India'),
(3, 'China');

-- --------------------------------------------------------

--
-- Table structure for table `loginhistory`
--

CREATE TABLE `loginhistory` (
  `id` int(11) NOT NULL,
  `userid` int(11) DEFAULT NULL,
  `login` varchar(50) DEFAULT NULL,
  `logintime` datetime DEFAULT CURRENT_TIMESTAMP,
  `machineip` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `loginhistory`
--

INSERT INTO `loginhistory` (`id`, `userid`, `login`, `logintime`, `machineip`) VALUES
(1, 1, 'admin1', '2018-03-23 22:57:22', '192.168.1.6'),
(2, 1, 'admin1', '2018-03-23 22:58:23', '::1'),
(3, 1, 'admin1', '2018-03-23 23:02:06', ''),
(4, 1, 'admin1', '2018-03-23 23:02:30', '192.168.1.6'),
(5, 1, 'admin1', '2018-03-23 23:05:20', '192.168.1.6'),
(6, 1, 'admin1', '2018-03-23 23:12:47', '192.168.1.6'),
(7, 1, 'admin1', '2018-03-23 23:13:03', '192.168.1.6'),
(8, 1, 'admin1', '2018-03-23 23:13:50', '192.168.1.6'),
(9, 1, 'admin1', '2018-03-23 23:14:30', '192.168.1.6'),
(10, 1, 'admin1', '2018-03-23 23:21:12', '192.168.1.6'),
(11, 1, 'admin1', '2018-03-23 23:21:57', '192.168.1.6'),
(12, 1, 'admin1', '2018-03-23 23:34:55', '192.168.1.6'),
(13, 1, 'admin1', '2018-03-23 23:38:34', '192.168.1.6'),
(14, 1, 'admin1', '2018-03-23 23:45:43', '192.168.1.6'),
(15, 1, 'admin1', '2018-03-23 23:52:18', '192.168.1.6'),
(16, 1, 'admin1', '2018-03-24 00:19:27', '192.168.1.6'),
(17, 1, 'admin1', '2018-03-24 00:29:35', '192.168.1.6'),
(18, 1, 'admin1', '2018-03-24 00:30:39', '192.168.1.6'),
(19, 1, 'admin1', '2018-03-24 00:34:59', '192.168.1.6'),
(20, 1, 'admin1', '2018-03-24 00:48:29', '192.168.1.6'),
(21, 1, 'admin1', '2018-03-24 00:49:07', '192.168.1.6'),
(22, 3, 'user1', '2018-03-24 00:50:10', '192.168.1.6'),
(23, 3, 'user1', '2018-03-24 00:52:18', '192.168.1.6'),
(24, 1, 'admin1', '2018-03-24 00:53:14', '192.168.1.6'),
(25, 3, 'user1', '2018-03-24 00:53:25', '192.168.1.6'),
(26, 1, 'admin1', '2018-03-24 02:32:18', '192.168.1.6'),
(27, 3, 'user1', '2018-03-24 02:32:53', '192.168.1.6'),
(28, 4, 'user2', '2018-03-24 02:39:05', '192.168.1.6'),
(29, 1, 'admin1', '2018-03-24 03:15:40', '192.168.1.6'),
(30, 1, 'admin1', '2018-03-24 03:33:59', '192.168.1.6'),
(31, 1, 'admin1', '2018-03-24 11:24:02', '192.168.1.9'),
(32, 3, 'user1', '2018-03-24 11:26:09', '192.168.1.9'),
(33, 1, 'admin1', '2018-03-24 11:26:21', '192.168.1.9'),
(34, 1, 'admin1', '2018-03-24 14:03:32', '192.168.1.9'),
(35, 4, 'user2', '2018-03-24 15:28:28', '192.168.1.11'),
(36, 4, 'user2', '2018-03-24 15:33:19', '192.168.1.11'),
(37, 4, 'user2', '2018-03-24 15:34:33', '192.168.1.11'),
(38, 4, 'user2', '2018-03-24 15:36:04', '192.168.1.11'),
(39, 4, 'user2', '2018-03-24 16:43:08', '192.168.1.11'),
(40, 4, 'user2', '2018-03-24 16:44:57', '192.168.1.11'),
(41, 1, 'admin1', '2018-03-24 16:45:28', '192.168.1.11'),
(42, 3, 'user12', '2018-03-24 16:49:04', '192.168.1.11'),
(43, 1, 'admin1', '2018-03-24 17:45:09', '192.168.1.11'),
(44, 1, 'admin1', '2018-03-24 17:52:04', '192.168.1.11'),
(45, 29, 'admin', '2018-03-24 20:52:47', '192.168.1.11'),
(46, 30, 'admin', '2018-03-24 20:54:35', '192.168.1.11'),
(47, 31, 'admin', '2018-03-24 20:57:37', '192.168.1.11'),
(48, 32, 'lipton', '2018-03-24 22:19:08', '192.168.1.11'),
(49, 33, 'asd', '2018-03-24 22:19:35', '192.168.1.11'),
(50, 33, 'asd', '2018-03-24 22:21:14', '192.168.1.11'),
(51, 31, 'admin', '2018-03-24 23:00:46', '192.168.1.11'),
(52, 31, 'admin', '2018-03-25 01:34:48', '192.168.1.11'),
(53, 33, 'asd', '2018-03-25 02:08:50', '192.168.1.11'),
(54, 31, 'admin', '2018-03-25 02:09:05', '192.168.1.11'),
(55, 33, 'asd', '2018-03-25 02:09:20', '192.168.1.11'),
(56, 31, 'admin', '2018-03-25 02:21:33', '192.168.1.11'),
(57, 31, 'admin', '2018-03-25 11:03:56', '192.168.1.12'),
(58, 31, 'admin', '2018-03-25 13:58:13', '192.168.1.12'),
(59, 31, 'admin', '2018-03-25 13:58:27', '192.168.1.12'),
(60, 32, 'lipton', '2018-03-25 13:58:48', '192.168.1.12'),
(61, 33, 'asd', '2018-03-25 14:00:35', '192.168.1.12'),
(62, 31, 'admin', '2018-03-30 14:39:13', '127.0.0.1'),
(63, 31, 'admin', '2018-03-30 15:24:02', '192.168.0.148'),
(64, 31, 'admin', '2018-03-31 03:22:40', '192.168.1.9'),
(65, 31, 'admin', '2018-03-31 16:07:03', '192.168.1.8'),
(66, 31, 'admin', '2018-03-31 16:10:26', '192.168.1.8'),
(67, 60, 'admin22', '2018-04-01 01:47:12', '192.168.1.8'),
(68, 31, 'admin', '2018-04-01 01:47:33', '192.168.1.8'),
(69, 31, 'admin', '2018-04-01 11:32:08', '192.168.1.8'),
(70, 31, 'admin', '2018-04-01 12:10:23', ''),
(71, 31, 'admin', '2018-04-01 12:14:12', ''),
(72, 31, 'admin', '2018-04-01 12:16:04', '192.168.1.8'),
(73, 32, 'lipton', '2018-04-01 12:26:41', '192.168.1.8'),
(74, 62, 'saad', '2018-04-01 12:31:57', '192.168.1.8'),
(75, 31, 'admin', '2018-04-01 12:32:05', '192.168.1.8'),
(76, 62, 'saad', '2018-04-01 12:32:49', '192.168.1.8'),
(77, 31, 'admin', '2018-04-01 12:57:30', '192.168.1.8'),
(78, 62, 'saad', '2018-04-01 13:20:07', '192.168.1.8'),
(79, 31, 'admin', '2018-04-01 13:32:34', '192.168.1.8'),
(80, 31, 'admin', '2018-04-01 13:32:59', '192.168.1.8'),
(81, 31, 'admin', '2018-04-07 19:38:39', '192.168.1.8'),
(82, 31, 'admin', '2018-04-30 16:00:27', '169.254.24.102'),
(83, 31, 'admin', '2018-04-30 16:01:11', '169.254.24.102'),
(84, 31, 'admin', '2018-07-08 23:22:30', '192.168.1.7'),
(85, 31, 'admin', '2018-07-08 23:58:39', '192.168.1.8'),
(86, 31, 'admin', '2018-07-10 22:31:06', '192.168.1.11'),
(87, 31, 'admin', '2018-07-10 23:02:01', '192.168.1.11'),
(88, 31, 'admin', '2018-07-10 23:04:00', '192.168.1.11'),
(89, 31, 'admin', '2018-07-10 23:14:07', '192.168.1.11'),
(90, 31, 'admin', '2018-07-10 23:22:24', '192.168.1.11');

-- --------------------------------------------------------

--
-- Table structure for table `permissions`
--

CREATE TABLE `permissions` (
  `permissionid` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `description` varchar(45) DEFAULT NULL,
  `createdon` datetime DEFAULT CURRENT_TIMESTAMP,
  `createdby` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `permissions`
--

INSERT INTO `permissions` (`permissionid`, `name`, `description`, `createdon`, `createdby`) VALUES
(5, 'murder', 'encounter,bloodshed', '2018-03-24 23:05:05', 31),
(7, 'play', 'asd', '2018-03-25 01:36:04', 31),
(8, 'fight', 'asdasd', '2018-03-25 01:36:12', 31),
(9, 'arrest', 'asda', '2018-03-25 01:36:31', 31),
(11, 'suicide', 'asdas', '2018-04-01 13:18:10', 31);

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `roleid` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `description` varchar(45) DEFAULT NULL,
  `createdon` datetime DEFAULT CURRENT_TIMESTAMP,
  `createdby` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`roleid`, `name`, `description`, `createdon`, `createdby`) VALUES
(5, 'teacher', 'teach,fail,pass', '2018-03-24 22:16:48', 31),
(6, 'student', 'sdds', '2018-04-01 02:33:58', 31),
(8, 'vendor', 'asdasd', '2018-04-01 02:40:22', 31),
(9, 'boy', 'play', '2018-04-01 02:50:48', 31);

-- --------------------------------------------------------

--
-- Table structure for table `role_permission`
--

CREATE TABLE `role_permission` (
  `id` int(11) NOT NULL,
  `roleid` int(11) DEFAULT NULL,
  `permissionid` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `role_permission`
--

INSERT INTO `role_permission` (`id`, `roleid`, `permissionid`) VALUES
(8, 6, 5),
(14, 5, 5),
(15, 8, 5),
(16, 9, 7),
(17, 5, 11),
(18, 6, 11);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `userid` int(11) NOT NULL,
  `login` varchar(45) DEFAULT NULL,
  `password` varchar(45) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `countryid` int(11) DEFAULT NULL,
  `createdon` datetime DEFAULT CURRENT_TIMESTAMP,
  `createdby` int(11) DEFAULT NULL,
  `isadmin` int(1) NOT NULL DEFAULT '0',
  `cityid` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`userid`, `login`, `password`, `name`, `email`, `countryid`, `createdon`, `createdby`, `isadmin`, `cityid`) VALUES
(31, 'admin', 'admin', 'umer', 'admin@d.com', 1, '2018-03-24 20:57:25', NULL, 1, 1),
(32, 'lipton', 'asd', 'khan', 'lipton@c.com', 3, '2018-03-24 20:58:02', 31, 1, 7),
(48, 'sdfsf', 'sgdfsgdgf', 'sdsfsf', 'sadas@asd.vom', 1, '2018-03-31 19:44:38', 31, 0, 1),
(49, 'hasan', 'asd', 'hasdan', 'asd@n3a.com', 3, '2018-03-31 20:03:23', 31, 0, 7),
(51, 'umerdd', 'asdasd', 'iftikhar', 'asdasd2@sd.com', 1, '2018-03-31 20:15:04', 31, 1, 1),
(52, 'asdasd', 'dasdasd', 'asdad', 'asda', 1, '2018-03-31 20:16:22', 31, 0, 1),
(56, 'dfgdg', 'sdfdsf', 'asdasd', 'asd@n.com', 1, '2018-03-31 20:35:24', 31, 0, 1),
(58, 'fhgf', 'sdfs', 'dsfdsf', 'sdfs', 1, '2018-03-31 20:41:44', 31, 0, 1),
(59, 'dfdfg', 'dfsdf', 'asdasd', 'asd@n33.com', 1, '2018-03-31 22:08:20', 31, 0, 1),
(60, 'admin22', 'admin22', 'umerasd', 'asdas3@ss.ocm', 1, '2018-04-01 01:33:19', 31, 0, 1),
(61, 'sdgmk', 'ksdfkj', 'skdfkw', 'ksdfk', 1, '2018-04-01 02:49:28', 31, 0, 1),
(62, 'saad', 'asd', 'sadd', 'asd@naa.com', 2, '2018-04-01 12:28:12', 32, 0, 4),
(63, 'lupi', 'singh', 'lupi', 'lupi@s.com', 1, '2018-04-07 19:39:25', 31, 0, NULL),
(66, 'dsfgf', 'sfgdfgdf', '<script>window.location.href=\"google.com\"</sc', 'asfdsf', 3, '2018-07-08 23:57:42', 31, 1, 7),
(67, 'kakak', 'umer', '<script>document.location=\"http://localhost/c', 'adasd@dsf.com', 1, '2018-07-10 22:35:36', 31, 1, 1),
(68, 'vbvb', 'xcvx', '<script>document.write(â€˜<img src=\"http://lo', 'xcvx@c.ovm', 1, '2018-07-10 22:42:29', 31, 1, 1),
(69, 'jsdfjs', 'sdfkgdfk', '<script>document.location=\"http://localhost/c', 'ssf@sdf.com', 1, '2018-07-10 22:47:15', 31, 1, 1),
(73, 'dfgdgjhjk', 'ldfjgdgk', '<script>document.location=\"http://localhost/c', 'dsfj@fvfv.bvom', 1, '2018-07-10 23:19:20', 31, 1, 1),
(75, 'fdfl', 'dfjdf', '<script>document.location=\"http://localhost/c', 'sdf@sdf.com', 1, '2018-07-10 23:23:48', 31, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `user_role`
--

CREATE TABLE `user_role` (
  `id` int(11) NOT NULL,
  `userid` int(11) DEFAULT NULL,
  `roleid` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user_role`
--

INSERT INTO `user_role` (`id`, `userid`, `roleid`) VALUES
(3, 31, 6),
(6, 32, 8),
(8, 31, 8),
(12, 62, 8),
(13, 62, 6),
(14, 62, 8),
(15, 62, 9);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `city`
--
ALTER TABLE `city`
  ADD PRIMARY KEY (`id`),
  ADD KEY `countryid` (`countryid`);

--
-- Indexes for table `country`
--
ALTER TABLE `country`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `loginhistory`
--
ALTER TABLE `loginhistory`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `permissions`
--
ALTER TABLE `permissions`
  ADD PRIMARY KEY (`permissionid`),
  ADD KEY `createdby` (`createdby`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`roleid`),
  ADD KEY `createdby` (`createdby`);

--
-- Indexes for table `role_permission`
--
ALTER TABLE `role_permission`
  ADD PRIMARY KEY (`id`),
  ADD KEY `permissionid` (`permissionid`),
  ADD KEY `roleid` (`roleid`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`userid`),
  ADD KEY `users_ibfk_1` (`countryid`),
  ADD KEY `users_ibfk_2` (`createdby`),
  ADD KEY `cityid` (`cityid`);

--
-- Indexes for table `user_role`
--
ALTER TABLE `user_role`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_role_ibfk_1` (`roleid`),
  ADD KEY `user_role_ibfk_2` (`userid`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `city`
--
ALTER TABLE `city`
  MODIFY `id` int(50) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT for table `country`
--
ALTER TABLE `country`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `loginhistory`
--
ALTER TABLE `loginhistory`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=91;
--
-- AUTO_INCREMENT for table `permissions`
--
ALTER TABLE `permissions`
  MODIFY `permissionid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `roleid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT for table `role_permission`
--
ALTER TABLE `role_permission`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `userid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=76;
--
-- AUTO_INCREMENT for table `user_role`
--
ALTER TABLE `user_role`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `city`
--
ALTER TABLE `city`
  ADD CONSTRAINT `city_ibfk_1` FOREIGN KEY (`countryid`) REFERENCES `country` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `permissions`
--
ALTER TABLE `permissions`
  ADD CONSTRAINT `permissions_ibfk_1` FOREIGN KEY (`createdby`) REFERENCES `users` (`userid`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `roles`
--
ALTER TABLE `roles`
  ADD CONSTRAINT `roles_ibfk_1` FOREIGN KEY (`createdby`) REFERENCES `users` (`userid`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `role_permission`
--
ALTER TABLE `role_permission`
  ADD CONSTRAINT `role_permission_ibfk_1` FOREIGN KEY (`permissionid`) REFERENCES `permissions` (`permissionid`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `role_permission_ibfk_2` FOREIGN KEY (`roleid`) REFERENCES `roles` (`roleid`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`countryid`) REFERENCES `country` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `users_ibfk_2` FOREIGN KEY (`createdby`) REFERENCES `users` (`userid`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `users_ibfk_3` FOREIGN KEY (`cityid`) REFERENCES `city` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `user_role`
--
ALTER TABLE `user_role`
  ADD CONSTRAINT `user_role_ibfk_1` FOREIGN KEY (`roleid`) REFERENCES `roles` (`roleid`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `user_role_ibfk_2` FOREIGN KEY (`userid`) REFERENCES `users` (`userid`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
