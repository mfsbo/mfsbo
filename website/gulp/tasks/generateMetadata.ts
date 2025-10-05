import { src, dest, TaskFunction } from 'gulp';
import replace from 'gulp-replace';
import rename from 'gulp-rename';
import pkgj from './../../package.json';
import gitrev from 'git-rev-sync';

const siteMetadata = {
  app_info_name: 'mfsbo',
  app_info_released_on: new Date().toISOString(),
  app_info_commitShortHash: gitrev.short('./../'),
  app_info_version: pkgj.version,
};

const templatePath = './../website/templates/metadata.template';
const outputPath = './../website/.vitepress/theme/';

// create a gulp task to generate metadata.ts from template
export const generateMetadata: TaskFunction = () => {
  return src(templatePath)
    //replace each {{ }} in template with values from siteMetadata
    .pipe(replace('{{app_info_name}}', siteMetadata.app_info_name))
    .pipe(replace('{{app_info_released_on}}', siteMetadata.app_info_released_on))
    .pipe(replace('{{app_info_commitShortHash}}', siteMetadata.app_info_commitShortHash))
    .pipe(replace('{{app_info_version}}', siteMetadata.app_info_version))
    .pipe(rename('metadata.ts'))
    .pipe(dest(outputPath));
};