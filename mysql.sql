SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for song
-- ----------------------------
DROP TABLE IF EXISTS `song`;
CREATE TABLE `song` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(128) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '����',
  `url` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '��ַ',
  `create_time` datetime DEFAULT NULL COMMENT '����ʱ��',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Table structure for song_tag
-- ----------------------------
DROP TABLE IF EXISTS `song_tag`;
CREATE TABLE `song_tag` (
  `song_id` int(11) NOT NULL COMMENT '����',
  `tag_id` int(11) NOT NULL COMMENT '��ǩ',
  PRIMARY KEY (`song_id`,`tag_id`),
  KEY `fk_song_tag_tag_1` (`tag_id`),
  CONSTRAINT `fk_song_tag_song_1` FOREIGN KEY (`song_id`) REFERENCES `song` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_song_tag_tag_1` FOREIGN KEY (`tag_id`) REFERENCES `tag` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Table structure for tag
-- ----------------------------
DROP TABLE IF EXISTS `tag`;
CREATE TABLE `tag` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `parent_id` int(11) DEFAULT NULL COMMENT '����ǩ',
  `name` varchar(128) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '����',
  PRIMARY KEY (`id`),
  KEY `fk_tag_tag_1` (`parent_id`),
  CONSTRAINT `fk_tag_tag_1` FOREIGN KEY (`parent_id`) REFERENCES `tag` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Table structure for topic
-- ----------------------------
DROP TABLE IF EXISTS `topic`;
CREATE TABLE `topic` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(255) DEFAULT NULL COMMENT '����',
  `cardtype` enum('��Ƶ','ͼ��01','ͼ��02','����') DEFAULT NULL COMMENT '��Ƭ����',
  `carddata` text COMMENT '��Ƭ��Ⱦ����',
  `content` text COMMENT '����',
  `clicks` bigint(20) unsigned DEFAULT NULL COMMENT '�������',
  `create_time` datetime DEFAULT NULL COMMENT '����ʱ��',
  `update_time` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  `order_time` datetime DEFAULT NULL COMMENT '����ʱ��',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;