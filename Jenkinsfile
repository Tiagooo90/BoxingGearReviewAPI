pipeline {
    agent any
    
    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/Tiagooo90/BoxingGearReviewAPI.git'
            }
        }
        
        stage('Restore Dependencies') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }
        
        stage('Run Tests') {
            steps {
                sh 'dotnet test'
            }
        }
        
        stage('Publish') {
            steps {
                sh 'dotnet publish --configuration Release --output ./publish'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploy Step - Configura o deploy para o teu servidor!'
            }
        }
    }
}
