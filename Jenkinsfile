pipeline {
    agent {
        node {
            label 'built-in'
        }
    }
    triggers {
        pollSCM '* * * * *'
    }
    stages {
        stage('Build') {
            tools {
                dotnetsdk 'dotnet-sdk-6.0'
            }
            steps {
                echo 'Building..'
                sh '''
                dotnet build User.sln
                '''
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
                sh '''
                echo "Testing Successfull.."
                '''
            }
        }
        stage('Deliver') {
            steps {
                echo 'Deliver....'
                sh '''
                echo "doing delivery stuff.."
                '''
            }
        }
    }
}
